using Application.Features.Cars.Rules;
using Application.Services.Repositories;
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
    public class UpdateCarStateCommand : IRequest<Car>
    {
        public int Id { get; set; }
        public CarState CarState { get; set; }

        public class UpdateCarStateCommandHandler : IRequestHandler<UpdateCarStateCommand, Car>
        {
            ICarRepository _carRepository;
            CarBusinessRules _carBusinessRules;

            public UpdateCarStateCommandHandler(ICarRepository carRepository,
                 CarBusinessRules carBusinessRules)
            {
                _carRepository = carRepository;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<Car> Handle(UpdateCarStateCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetAsync(c => c.Id == request.Id);
                carToUpdate.CarState = request.CarState;

                var updatedCar = await _carRepository.UpdateAsync(carToUpdate);
                return updatedCar;
            }
        }
    }
}
