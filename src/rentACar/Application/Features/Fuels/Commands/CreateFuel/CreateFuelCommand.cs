using Application.Features.Fuels.Dtos;
using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.CreateFuel
{
    public class CreateFuelCommand : IRequest<CreateFuelDto>
    {
        public string Name { get; set; }

        public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, CreateFuelDto>
        {
            IFuelRepository _fuelRepository;
            IMapper _mapper;
            FuelBusinessRules _fuelBusinessRules;

            public CreateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper, FuelBusinessRules fuelBusinessRules)
            {
                _fuelRepository = fuelRepository;
                _mapper = mapper;
                _fuelBusinessRules = fuelBusinessRules;
            }

            public async Task<CreateFuelDto> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
            {
                //business rules
                await _fuelBusinessRules.FuelNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedFuel = _mapper.Map<Fuel>(request);

                var createdFuel = await _fuelRepository.AddAsync(mappedFuel);
                var fuelToReturn = _mapper.Map<CreateFuelDto>(createdFuel);
                return fuelToReturn;
            }
        }
    }
}
