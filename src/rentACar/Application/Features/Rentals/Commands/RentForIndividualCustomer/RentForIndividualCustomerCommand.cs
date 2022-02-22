using Application.Features.AdditionalServices.Dtos;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.CreditCardInfos.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Dtos;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.PromotionCodes.Rules;
using Application.Features.Rentals.Rules;
using Application.Services.AddtionalServiceService;
using Application.Services.CarService;
using Application.Services.Helpers;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.PaymentService;
using Application.Services.PromotionCodeService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.RentForIndividualCustomer;

public class RentForIndividualCustomerCommand : IRequest<CreateInvoiceDto>, ITransactionalRequest
{
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int RentCityId { get; set; }
    public int ReturnCityId { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public string? PromotionCode { get; set; }
    public List<int> AdditionalServiceIds { get; set; }
    public CreateCreditCardInfosDto CreditCardInfos { get; set; }

    public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForIndividualCustomerCommand, CreateInvoiceDto>
    {
        private readonly IRentalRepository rentalRepository;
        private readonly IMapper mapper;
        private readonly RentalBusinessRules rentalBusinessRules;
        private readonly IndividualCustomerBusinessRules individualCustomerBusinessRules;
        private readonly ICarService carService;
        private readonly IModelService modelService;
        private readonly IPaymentService paymentService;
        private readonly IInvoiceService invoiceService;
        private readonly IAdditionalServiceService additionalServiceService;
        private readonly PromotionCodeBusinessRules promotionCodeBusinessRules;
        private readonly IPromotionCodeService promotionCodeService;

        public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository,
            IMapper mapper,
            RentalBusinessRules rentalBusinessRules,
            IndividualCustomerBusinessRules individualCustomerBusinessRules,
            ICarService carService,
            IPaymentService paymentService,
            IModelService modelService,
            IInvoiceService invoiceService,
            IAdditionalServiceService additionalServiceService,
            PromotionCodeBusinessRules promotionCodeBusinessRules,
            IPromotionCodeService promotionCodeService)
        {
            this.rentalRepository = rentalRepository;
            this.mapper = mapper;
            this.rentalBusinessRules = rentalBusinessRules;
            this.individualCustomerBusinessRules = individualCustomerBusinessRules;
            this.carService = carService;
            this.paymentService = paymentService;
            this.modelService = modelService;
            this.invoiceService = invoiceService;
            this.additionalServiceService = additionalServiceService;
            this.promotionCodeBusinessRules = promotionCodeBusinessRules;
            this.promotionCodeService = promotionCodeService;
        }

        public async Task<CreateInvoiceDto> Handle(RentForIndividualCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var car = await this.carService.GetCarById(request.CarId);
            this.rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
            this.rentalBusinessRules.CheckIfCarIsRented(request.CarId);
            await this.individualCustomerBusinessRules.CheckIfIndividualCustomerExists(request.CustomerId);
            await this.rentalBusinessRules.CheckIfICFindexScoreIsEnough(request.CarId, request.CustomerId);
            
            if(request.PromotionCode is not null && request.PromotionCode != "") 
            {
                await this.promotionCodeBusinessRules.CheckIfPromotionCodeExists(request.PromotionCode);
                await this.promotionCodeBusinessRules.CheckIfPromotionCodeDateIsValid(request.PromotionCode);
                await this.promotionCodeBusinessRules.CheckIfPromotionCodeIsUsed(request.PromotionCode, request.CustomerId);
            }

            var mappedRental = this.mapper.Map<Rental>(request);
            mappedRental.RentedKilometer = car.Kilometer;
            mappedRental.AdditionalServices = await this.additionalServiceService.GetAdditionalServicesByIdList(request.AdditionalServiceIds);
            var createdRental = await this.rentalRepository.AddAsync(mappedRental);

            UpdateCarStateCommand command = new UpdateCarStateCommand
            {
                Id = request.CarId,
                CarState = CarState.Rented
            };
            await this.carService.UpdateCarState(command);

            var discountRate = 0;
            if(request.PromotionCode is not null && request.PromotionCode != "")
            {
                discountRate = await this.promotionCodeService.GetPromotionCodeDiscount(request.PromotionCode);
                await this.promotionCodeService.UsePromotionCodeForCustomer(request.PromotionCode, request.CustomerId);
            }            

            var calculatedTotalSum = await TotalSumCalculator.CalculateTotalSumForIC(request, car, mappedRental.AdditionalServices.ToList(), modelService, discountRate);

            CreatePaymentCommand paymentCommand = new CreatePaymentCommand
            {
                CreditCardInfos = request.CreditCardInfos,
                PaymentDate = DateTime.Now,
                RentalId = createdRental.Id,
                TotalSum = calculatedTotalSum
            };
            await this.paymentService.MakePayment(paymentCommand);

            //TODO: Invoice No oluşturan bir Helper sınıfı yazalım.
            CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
            {
                CustomerId = request.CustomerId,
                InvoiceDate = DateTime.Now,
                InvoiceNo = 20220001,
                RentalId = createdRental.Id,
                TotalSum = calculatedTotalSum
            };

            var invoiceResult = await this.invoiceService.MakeOutInvoice(invoiceCommand);

            foreach (var additionalService in mappedRental.AdditionalServices.ToList())
            {
                invoiceResult.AdditionalServices.Add(this.mapper.Map<AdditionalServiceListDto>(additionalService));
            };

            return invoiceResult;
        }

        
    }
}

