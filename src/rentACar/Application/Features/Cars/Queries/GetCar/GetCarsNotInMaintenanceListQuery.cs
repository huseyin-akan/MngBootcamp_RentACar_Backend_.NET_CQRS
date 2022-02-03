using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Utilities.Results;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetCar
{
    public class GetCarsNotInMaintenanceListQuery : IRequest<DataResult<CarListModel>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetCarsNotInMaintenanceListQueryHandler : IRequestHandler<GetCarsNotInMaintenanceListQuery, DataResult<CarListModel>>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            public GetCarsNotInMaintenanceListQueryHandler(ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }
            public async Task<DataResult<CarListModel>> Handle(GetCarsNotInMaintenanceListQuery request, CancellationToken cancellationToken)
            {
                var cars = await _carRepository.GetListAsync(
                    c => c.CarState != CarState.InMaintenance,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCars = _mapper.Map<CarListModel>(cars);
                return new SuccessDataResult<CarListModel>(mappedCars);
            }
        }
    }



}
