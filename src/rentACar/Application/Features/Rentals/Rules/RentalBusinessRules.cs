using Application.Features.Maintenenaces.Rules;
using Application.Services.CarService;
using Application.Services.CustomerServices;
using Application.Services.FindexScoreService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
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
        readonly IFindexScoreService _findexScoreService;
        readonly ICarService _carService;
        readonly IIndividualCustomerService _individualCustomerService;
        readonly ICorporateCustomerService _corporateCustomerService;

        public RentalBusinessRules(IRentalRepository rentalRepository,
            MaintenanceBusinessRules maintenanceBusinessRules,
            IFindexScoreService findexScoreService,
            ICarService carService,
            IIndividualCustomerService individualCustomerService,
            ICorporateCustomerService corporateCustomerService)
        {
            _rentalRepository = rentalRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
            _findexScoreService = findexScoreService;
            _carService = carService;
            _individualCustomerService = individualCustomerService;
            _corporateCustomerService = corporateCustomerService;
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

        public async Task<bool> CheckIfICFindexScoreIsEnough(int carId, int customerId)
        {
            var nationalId = await this._individualCustomerService.GetNationalId(customerId);
            var carFindexScore = await this._carService.GetFindexScoreById(carId);
            var findexScore = await this._findexScoreService.getICFindexScore(nationalId);

            if (findexScore < carFindexScore)
            {
                throw new BusinessException(Messages.FindexScoreNotEnough);
            }
            return true;
        }

        public async Task<bool> CheckIfCCFindexScoreIsEnough(int carId, int customerId)
        {
            var taxNumber = await this._corporateCustomerService.GetTaxNumber(customerId);
            var carFindexScore = await this._carService.GetFindexScoreById(carId);
            var findexScore = await this._findexScoreService.getICFindexScore(taxNumber);

            if(findexScore < carFindexScore)
            {
                throw new BusinessException(Messages.FindexScoreNotEnough);
            }
            return true;
        }

        public async Task CheckIfKilometerValid(int carId, int kilometer)
        {
            var carToCheck = await this._carService.GetCarById(carId);
            if(carToCheck is null)
            {
                throw new BusinessException(Messages.CarNotFound);
            }
            if(carToCheck.Kilometer > kilometer)
            {
                throw new BusinessException(Messages.KilometerCantBeLess);
            }
        }
        
    }

}
