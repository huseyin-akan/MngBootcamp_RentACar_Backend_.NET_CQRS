using Application.Features.Maintenenaces.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Commands.CreateMaintenance
{
    public class CreateMaintenanceCommand : IRequest<Maintenance>
    {
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int CarId { get; set; }

        public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, Maintenance>
        {
            IMaintenanceRepository _maintenanceRepository;
            IMapper _mapper;
            MaintenanceBusinessRules _maintenanceBusinessRules;

            public CreateMaintenanceCommandHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper, MaintenanceBusinessRules maintenanceBusinessRules)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
                _maintenanceBusinessRules = maintenanceBusinessRules;
            }

            public async Task<Maintenance> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                _maintenanceBusinessRules.CheckIfCarIsRented(request.CarId);

                var mappedMaintenance = _mapper.Map<Maintenance>(request);

                var createdMaintenance = await _maintenanceRepository.AddAsync(mappedMaintenance);
                return createdMaintenance;
            }
        }
    }
}
