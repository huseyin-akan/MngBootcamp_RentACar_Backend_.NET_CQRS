using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetCar
{
    public class GetAllCarsListQuery : IRequest<CarListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetAllCarsListQueryHandler : IRequestHandler<GetAllCarsListQuery, CarListModel>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            public GetAllCarsListQueryHandler(ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }
            public async Task<CarListModel> Handle(GetAllCarsListQuery request, CancellationToken cancellationToken)
            {
                var cars = await _carRepository.GetAllCars(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCars = _mapper.Map<CarListModel>(cars);
                return mappedCars;
            }
        }
    }
}
