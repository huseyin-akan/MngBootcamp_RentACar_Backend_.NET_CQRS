using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Payments.Commands.CreatePayment;
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
            private readonly IRentalRepository rentalRepository;
            private readonly IMapper mapper;
            private readonly RentalBusinessRules rentalBusinessRules;
            private readonly IndividualCustomerBusinessRules individualCustomerBusinessRules;
            private readonly ICarService carService;

            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper,
                RentalBusinessRules rentalBusinessRules,
                IndividualCustomerBusinessRules individualCustomerBusinessRules,
                ICarService carService)
            {
                this.rentalRepository = rentalRepository;
                this.mapper = mapper;
                this.rentalBusinessRules = rentalBusinessRules;
                this.individualCustomerBusinessRules = individualCustomerBusinessRules;
                this.carService = carService;
            }

            //TODO: Bu metot kesinlikle transactional çalışmalı.
            public async Task<Rental> Handle(RentForIndividualCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this.carService.GetCarById(request.CarId);
                this.rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                this.rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await this.individualCustomerBusinessRules.CheckIfIndividualCustomerExists(request.CustomerId);
                await this.rentalBusinessRules.CheckIfICFindexScoreIsEnough(request.CarId, request.CustomerId);

                var mappedRental = this.mapper.Map<Rental>(request);
                mappedRental.RentedKilometer = car.Kilometer;

                var createdRental = await this.rentalRepository.AddAsync(mappedRental);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this.carService.UpdateCarState(command);

                return createdRental;
            }
        }
    }
}
