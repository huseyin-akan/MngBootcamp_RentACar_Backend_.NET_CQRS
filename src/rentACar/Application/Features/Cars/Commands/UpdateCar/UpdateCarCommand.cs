using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
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
    public class UpdateCarCommand : IRequest<Car>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public int FindexScore { get; set; }
        public int Kilometer { get; set; }
        public CarState CarState { get; set; }

        public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, Car>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public UpdateCarCommandHandler(ICarRepository carRepository,
                IMapper mapper, CarBusinessRules carBusinessRules)
            {
                _carRepository = carRepository;
                _mapper = mapper;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetAsync(c => c.Id == request.Id);
                if (carToUpdate is null)
                {
                    throw new RepositoryException(Messages.CarNotFound);
                }
                var mappedCar = _mapper.Map(request, carToUpdate);

                var updatedCar = await _carRepository.UpdateAsync(mappedCar);
                return updatedCar;
            }
        }
    }
}
