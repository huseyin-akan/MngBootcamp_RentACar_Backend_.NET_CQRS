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
        readonly IMaintenanceRepository _maintenanceRepository;

        public RentalBusinessRules(IRentalRepository rentalRepository, IMaintenanceRepository maintenanceRepository)
        {
            _rentalRepository = rentalRepository;
            _maintenanceRepository = maintenanceRepository;
        }

        //Gerkhin
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
