using Application.Features.IndividualCustomers.Rules;
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
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
        public int RentedKilometer { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForIndividualCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper, RentalBusinessRules rentalBusinessRules,
                IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<Rental> Handle(RentForIndividualCustomerCommand request,
                CancellationToken cancellationToken)
            {
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfICFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _individualCustomerBusinessRules.CheckIfIndividualCustomerExists(request.CustomerId);

                var mappedRental = _mapper.Map<Rental>(request);

                var createdRental = await _rentalRepository.AddAsync(mappedRental);
                return createdRental;
            }
        }
    }
}
