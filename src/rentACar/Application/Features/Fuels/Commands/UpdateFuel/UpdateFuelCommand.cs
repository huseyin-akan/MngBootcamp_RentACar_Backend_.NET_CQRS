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

namespace Application.Features.Fuels.Commands.UpdateFuel
{
    public class UpdateFuelCommand : IRequest<UpdateFuelDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, UpdateFuelDto>
        {
            IFuelRepository _fuelRepository;
            IMapper _mapper;
            FuelBusinessRules _fuelBusinessRules;

            public UpdateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper, FuelBusinessRules fuelBusinessRules)
            {
                _fuelRepository = fuelRepository;
                _mapper = mapper;
                _fuelBusinessRules = fuelBusinessRules;
            }

            public async Task<UpdateFuelDto> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
            {
                await _fuelBusinessRules.FuelNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedFuel = _mapper.Map<Fuel>(request);

                var updatedFuel = await _fuelRepository.UpdateAsync(mappedFuel);
                var fuelToReturn = _mapper.Map<UpdateFuelDto>(updatedFuel);
                return fuelToReturn;
            }
        }

    }
}
