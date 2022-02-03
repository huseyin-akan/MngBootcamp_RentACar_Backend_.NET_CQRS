using Application.Features.Rentals.Rules;
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
        readonly Lazy<RentalBusinessRules> _rentalBusinessRules;

        public MaintenanceBusinessRules(IMaintenanceRepository maintenanceRepository,
            Lazy<RentalBusinessRules> rentalBusinessRules
            )
        {
            _maintenanceRepository = maintenanceRepository;
            _rentalBusinessRules = rentalBusinessRules;
        }

        public bool CheckIfCarIsUnderMaintenance(int carId)
        {
            var result = _maintenanceRepository.CheckIfCarIsUnderMaintenance(carId);

            if (result)
            {
                throw new BusinessException("Car is under maintenance.");
            }
            return result;
        }

        public bool CheckIfCarIsRented(int carId)
        {
            var result = _rentalBusinessRules.Value.CheckIfCarIsRented(carId);

            return result;
        }
    }
}
