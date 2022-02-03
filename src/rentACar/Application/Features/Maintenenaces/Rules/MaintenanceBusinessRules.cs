using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Rules
{
    public class MaintenanceBusinessRules
    {
        readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceBusinessRules(IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }

        public bool CheckIfCarIsUnderMaintenance(int carId)
        {
            var result = _maintenanceRepository.CheckIfCarIsUnderMaintenance(carId);

            if (result)
            {
                throw new BusinessException("Car is under maintenance. Therefore cannot be rent!!!");
            }
            return result;
        }
    }
}
