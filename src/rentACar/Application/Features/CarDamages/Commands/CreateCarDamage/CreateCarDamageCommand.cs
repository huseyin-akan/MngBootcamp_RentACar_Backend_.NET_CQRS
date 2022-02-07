using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Commands.CreateCarDamage
{

    public class CreateCarDamageCommand : IRequest<CarDamage>
    {
        public int CarId { get; set; }
        public string Description { get; set; }

        public class CreateCarDamageCommandHandler : IRequestHandler<CreateCarDamageCommand, CarDamage>
        {
            ICarDamageRepository _carDamageRepository;
            IMapper _mapper;
            CarDamageBusinessRules _carDamageBusinessRules;

            public CreateCarDamageCommandHandler(ICarDamageRepository carDamageRepository,
                IMapper mapper,
                CarDamageBusinessRules carDamageBusinessRules)
            {
                _carDamageRepository = carDamageRepository;
                _mapper = mapper;
                _carDamageBusinessRules = carDamageBusinessRules;
            }

            public async Task<CarDamage> Handle(CreateCarDamageCommand request, CancellationToken cancellationToken)
            {
                var mappedCarDamage = _mapper.Map<CarDamage>(request);

                var createdCarDamage = await _carDamageRepository.AddAsync(mappedCarDamage);
                return createdCarDamage;
            }
        }
    }
}
