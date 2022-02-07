using Application.Features.CorporateCustomers.Rules;
using Application.Features.Rentals.Rules;
using Application.Services.Managers;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
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
        public int RentedKilometer { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForCorporateCustomerCommandHandler : IRequestHandler<RentForCorporateCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;            
            public RentForCorporateCustomerCommandHandler(IRentalRepository rentalRepository,
                IMapper mapper, RentalBusinessRules rentalBusinessRules,
                CorporateCustomerBusinessRules corporateCustomerBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            }

            public async Task<Rental> Handle(RentForCorporateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfCCFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _corporateCustomerBusinessRules.CheckIfCorporateCustomerExists(request.CustomerId);

                var mappedRental = _mapper.Map<Rental>(request);

                var createdRental = await _rentalRepository.AddAsync(mappedRental);
                return createdRental;
            }
        }
    }
}
