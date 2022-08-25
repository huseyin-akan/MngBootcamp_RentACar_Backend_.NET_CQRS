using Application.Features.Cars.Commands.UpdateCar;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private IMediator _mediator;
        public CarService(ICarRepository carRepository, IMediator mediator)
        {
            _carRepository = carRepository;
            _mediator = mediator;
        }

        public async Task<Domain.Entities.Car> GetCarById(int id)
        {
            var carToReturn = await _carRepository.GetAsync(c => c.Id == id);
            if (carToReturn is null)
            {
                throw new BusinessException(Messages.CarNotFound);
            }
            return carToReturn;
        }

        public async Task<int> GetFindexScoreById(int id)
        {
            var result = await _carRepository.GetAsync(c => c.Id == id);
            if(result is null)
            {
                throw new BusinessException(Messages.CarNotFound);
            }
            return result.FindexScore;
        }

        public async Task UpdateCarAfterRentalEnd(UpdateCarAfterRentalEndCommand command)
        {
            var result = await this._mediator.Send(command);

            if (result is null)
            {
                throw new BusinessException(Messages.CarUpdateFailed);
            }
        }

        public async Task UpdateCarState(UpdateCarStateCommand command)
        {           
            var result = await _mediator.Send(command);

            if (result is null)
            {
                throw new BusinessException(Messages.CarUpdateFailed);
            }

        }
    }
}
