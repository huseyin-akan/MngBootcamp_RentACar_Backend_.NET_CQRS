using Application.Features.Rentals.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Queries.GetRental
{
    public class GetActiveRentalsListQuery : IRequest<IPaginate<ActiveRentalsListDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetActiveRentalsListQueryHandler : IRequestHandler<GetActiveRentalsListQuery, IPaginate<ActiveRentalsListDto>>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;

            public GetActiveRentalsListQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
            }

            public async Task<IPaginate<ActiveRentalsListDto>> Handle(GetActiveRentalsListQuery request, CancellationToken cancellationToken)
            {
                var rentals = await _rentalRepository.GetActiveRentals(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                return rentals;
            }
        }
    }
}
