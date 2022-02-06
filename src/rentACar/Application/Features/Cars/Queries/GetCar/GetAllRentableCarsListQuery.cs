using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetCar
{
    public class GetAllRentableCarsListQuery : IRequest<CarListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetAllRentableCarsListQueryHandler : IRequestHandler<GetAllRentableCarsListQuery, CarListModel>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            public GetAllRentableCarsListQueryHandler(ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }
            public async Task<CarListModel> Handle(GetAllRentableCarsListQuery request, CancellationToken cancellationToken)
            {
                var cars = await _carRepository.GetListAsync(
                    c => c.CarState == CarState.Available,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCars = _mapper.Map<CarListModel>(cars);
                return mappedCars;
            }
        }
    }
}
