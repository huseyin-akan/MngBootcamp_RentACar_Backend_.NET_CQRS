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

namespace Application.Features.Rentals.Commands.CreateRental
{
    public class CreateRentalCommand : IRequest<Rental>
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }
        public int CarId { get; set; }

        public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;

            public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
            }

            public async Task<Rental> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
            {
                //TODO: business rules + result sistem eklenecek.
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);                

                var mappedRental = _mapper.Map<Rental>(request);

                var createdRental = await _rentalRepository.AddAsync(mappedRental);
                return createdRental;
            }
        }
    }
}
