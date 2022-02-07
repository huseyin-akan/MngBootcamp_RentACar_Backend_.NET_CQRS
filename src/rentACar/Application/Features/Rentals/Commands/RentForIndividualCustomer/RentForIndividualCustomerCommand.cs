using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Rentals.Rules;
using Application.Services.Managers.Abstract;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.RentForIndividualCustomer
{
    public class RentForIndividualCustomerCommand : IRequest<Rental>
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForIndividualCustomerCommand, Rental>
        {
            private readonly IRentalRepository _rentalRepository;
            private readonly IMapper _mapper;
            private readonly RentalBusinessRules _rentalBusinessRules;
            private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            private readonly ICarService carService;
            private readonly IInvoiceService invoiceService;
            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper,
                RentalBusinessRules rentalBusinessRules,
                IndividualCustomerBusinessRules individualCustomerBusinessRules,
                ICarService carService, IInvoiceService invoiceService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
                this.carService = carService;
                this.invoiceService = invoiceService;
            }

            public async Task<Rental> Handle(RentForIndividualCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this.carService.GetCarById(request.CarId);
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfICFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _individualCustomerBusinessRules.CheckIfIndividualCustomerExists(request.CustomerId);
                
                //CheckIfPaymentIsSuccessful
                //TODO: FakePosSystemService implementasyonu PaymentService içerisinde yapılacak.

                var mappedRental = _mapper.Map<Rental>(request);                
                mappedRental.RentedKilometer = car.Kilometer;

                var createdRental = await _rentalRepository.AddAsync(mappedRental);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this.carService.UpdateCarState(command);

                //TODO: Invoice No oluşturan bir Helper sınıfı yazalım.
                //TODO: TotalSum Payment yapıldıktan sonra alınacak.
                CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
                {
                    CustomerId = request.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = 123,
                    RentalId = createdRental.Id,
                    TotalSum = 500
                };
                await this.invoiceService.MakeOutInvoice(invoiceCommand);

                //TODO: Fatura oluşturduktan sonra invoicelistdto döndürülecek.
                return createdRental;
            }
        }
    }
}
