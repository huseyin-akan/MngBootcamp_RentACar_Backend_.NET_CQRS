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

namespace Application.Features.Maintenenaces.Commands.UpdateMaintenance
{
    public class UpdateMaintenanceCommand : IRequest<Maintenance>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int CarId { get; set; }

        public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, Maintenance>
        {
            IMaintenanceRepository _maintenanceRepository;
            IMapper _mapper;
            MaintenanceBusinessRules _maintenanceBusinessRules;

            public UpdateMaintenanceCommandHandler(IMaintenanceRepository maintenanceRepository,
                IMapper mapper, MaintenanceBusinessRules maintenanceBusinessRules)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
                _maintenanceBusinessRules = maintenanceBusinessRules;
            }

            public async Task<Maintenance> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                _maintenanceBusinessRules.CheckIfCarIsRented(request.CarId);

                var mappedMaintenance = _mapper.Map<Maintenance>(request);

                var createdMaintenance = await _maintenanceRepository.UpdateAsync(mappedMaintenance);
                return createdMaintenance;
            }
        }
    }
}
