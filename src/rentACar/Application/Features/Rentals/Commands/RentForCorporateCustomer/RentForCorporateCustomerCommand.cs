using Application.Features.AdditionalServices.Dtos;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.CreditCardInfos.Dtos;
using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Dtos;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Rentals.Rules;
using Application.Services.AddtionalServiceService;
using Application.Services.CarService;
using Application.Services.Helpers;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.PaymentService;
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

namespace Application.Features.Rentals.Commands.RentForCorporateCustomer
{
    public class RentForCorporateCustomerCommand : IRequest<CreateInvoiceDto>, ITransactionalRequest
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public List<int> AdditionalServiceIds { get; set; }
        public CreateCreditCardInfosDto CreditCardInfos { get; set; }

        public class RentForCorporateCustomerCommandHandler : IRequestHandler<RentForCorporateCustomerCommand, CreateInvoiceDto>
        {
            private readonly IRentalRepository rentalRepository;
            private readonly IMapper mapper;
            private readonly RentalBusinessRules rentalBusinessRules;
            private readonly CorporateCustomerBusinessRules corporateCustomerBusinessRules;
            private readonly ICarService carService;
            private readonly IModelService modelService;
            private readonly IPaymentService paymentService;
            private readonly IInvoiceService invoiceService;
            private readonly IAdditionalServiceService additionalServiceService;

            public RentForCorporateCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper,
                RentalBusinessRules rentalBusinessRules,
                CorporateCustomerBusinessRules corporateCustomerBusinessRules,
                ICarService carService,
                IModelService modelService,
                IPaymentService paymentService,
                IInvoiceService invoiceService, IAdditionalServiceService additionalServiceService)
            {
                this.rentalRepository = rentalRepository;
                this.mapper = mapper;
                this.rentalBusinessRules = rentalBusinessRules;
                this.corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                this.carService = carService;
                this.modelService = modelService;
                this.paymentService = paymentService;
                this.invoiceService = invoiceService;
                this.additionalServiceService = additionalServiceService;
            }

            public async Task<CreateInvoiceDto> Handle(RentForCorporateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this.carService.GetCarById(request.CarId);
                rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await corporateCustomerBusinessRules.CheckIfCorporateCustomerExists(request.CustomerId);
                await rentalBusinessRules.CheckIfCCFindexScoreIsEnough(request.CarId, request.CustomerId);
                
                var mappedRental = mapper.Map<Rental>(request);
                mappedRental.RentedKilometer = car.Kilometer;
                mappedRental.AdditionalServices = await this.additionalServiceService.GetAdditionalServicesByIdList(request.AdditionalServiceIds);
                var createdRental = await rentalRepository.AddAsync(mappedRental);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this.carService.UpdateCarState(command);

                var calculatedTotalSum = await TotalSumCalculator.CalculateTotalSumForCC(request, car, mappedRental.AdditionalServices.ToList(), this.modelService);
  
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
                    InvoiceNo = 256,
                    RentalId = createdRental.Id,
                    TotalSum = 500
                };
                var invoiceResult =  await this.invoiceService.MakeOutInvoice(invoiceCommand);

                foreach (var additionalService in mappedRental.AdditionalServices.ToList())
                {
                    invoiceResult.AdditionalServices.Add(this.mapper.Map<AdditionalServiceListDto>(additionalService));
                };

                return invoiceResult;
            }
        }
    }
}
