using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Rentals.Rules;
using Application.Services.Managers;
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

namespace Application.Features.Rentals.Commands.RentForCorporateCustomer
{
    public class RentForCorporateCustomerCommand : IRequest<Rental>
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForCorporateCustomerCommandHandler : IRequestHandler<RentForCorporateCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            ICarService carService;
            private readonly IInvoiceService invoiceService;
            public RentForCorporateCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper, RentalBusinessRules rentalBusinessRules,
                CorporateCustomerBusinessRules corporateCustomerBusinessRules, ICarService carService, IInvoiceService invoiceService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                this.carService = carService;
                this.invoiceService = invoiceService;
            }

            public async Task<Rental> Handle(RentForCorporateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this.carService.GetCarById(request.CarId);
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfCCFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _corporateCustomerBusinessRules.CheckIfCorporateCustomerExists(request.CustomerId);

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
                    InvoiceNo = 256,
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
