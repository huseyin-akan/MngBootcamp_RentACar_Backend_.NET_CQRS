using Application.Features.Cars.Dtos;
using Application.Features.Cars.Rules;
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

namespace Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarStateCommand : IRequest<UpdateCarDto>
    {
        public int Id { get; set; }
        public CarState CarState { get; set; }

        public class UpdateCarStateCommandHandler : IRequestHandler<UpdateCarStateCommand, UpdateCarDto>
        {
            ICarRepository _carRepository;
            CarBusinessRules _carBusinessRules;
            IMapper _mapper;

            public UpdateCarStateCommandHandler(
                 ICarRepository carRepository,
                 CarBusinessRules carBusinessRules,
                 IMapper mapper)
            {
                _carRepository = carRepository;
                _carBusinessRules = carBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdateCarDto> Handle(UpdateCarStateCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetAsync(c => c.Id == request.Id);
                carToUpdate.CarState = request.CarState;

                var updatedCar = await _carRepository.UpdateAsync(carToUpdate);
                var carToReturn = _mapper.Map<UpdateCarDto>(updatedCar);
                return carToReturn;
            }
        }
    }
}
