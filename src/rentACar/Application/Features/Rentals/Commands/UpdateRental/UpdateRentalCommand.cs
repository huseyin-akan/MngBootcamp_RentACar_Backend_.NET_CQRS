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

namespace Application.Features.Rentals.Commands.UpdateRental
{
    public class UpdateRentalCommand : IRequest<Rental>
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }
        public int CarId { get; set; }

        public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            public UpdateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
            }

            public async Task<Rental> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
            {
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);

                var mappedRental = _mapper.Map<Rental>(request);

                var createdRental = await _rentalRepository.UpdateAsync(mappedRental);
                return createdRental;
            }
        }
    }
}
