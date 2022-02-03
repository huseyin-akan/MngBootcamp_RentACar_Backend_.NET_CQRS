using Application.Features.Maintenenaces.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Rules
{

    public class RentalBusinessRules
    {
        readonly IRentalRepository _rentalRepository;
        readonly MaintenanceBusinessRules _maintenanceBusinessRules;

        public RentalBusinessRules(IRentalRepository rentalRepository,
            MaintenanceBusinessRules maintenanceBusinessRules)
        {
            _rentalRepository = rentalRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
        }

        public bool CheckIfCarIsUnderMaintenance(int carId)
        {
            var result = _maintenanceBusinessRules.CheckIfCarIsUnderMaintenance(carId);

            return result;
        }

        public bool CheckIfCarIsRented(int carId)
        {
            var result = _rentalRepository.CheckIfCarIsRented(carId);

            if (result)
            {
                throw new BusinessException("Car is rented.");
            }
            return result;
        }
    }

}
