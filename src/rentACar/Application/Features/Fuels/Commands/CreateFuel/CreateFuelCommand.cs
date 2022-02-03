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
    public class CreateFuelCommand : IRequest<Fuel>
    {
        public string Name { get; set; }

        public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, Fuel>
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

            public async Task<Fuel> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
            {
                //business rules
                await _fuelBusinessRules.FuelNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedFuel = _mapper.Map<Fuel>(request);

                var createdFuel = await _fuelRepository.AddAsync(mappedFuel);
                return createdFuel;
            }
        }
    }
}
