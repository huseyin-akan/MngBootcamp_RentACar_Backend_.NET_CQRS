using Application.Features.Maintenenaces.Rules;
using Application.Services.Managers;
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
        readonly ICarRepository _carRepository;
        readonly IIndividualCustomerRepository individualCustomerRepository;
        readonly ICorporateCustomerRepository corporateCustomerRepository;

        public RentalBusinessRules(IRentalRepository rentalRepository,
            MaintenanceBusinessRules maintenanceBusinessRules, IFindexScoreService findexScoreService, ICarRepository carRepository, IIndividualCustomerRepository individualCustomerRepository, ICorporateCustomerRepository corporateCustomerRepository)
        {
            _rentalRepository = rentalRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
            _findexScoreService = findexScoreService;
            _carRepository = carRepository;
            this.individualCustomerRepository = individualCustomerRepository;
            this.corporateCustomerRepository = corporateCustomerRepository;
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
            var customer = await this.individualCustomerRepository.GetAsync(i => i.Id == customerId);
            var car = await this._carRepository.GetAsync(c => c.Id == carId);
            var findexScore = await this._findexScoreService.getICFindexScore(customer.NationalId);

            if (findexScore < car.FindexScore)
            {
                throw new BusinessException(Messages.FindexScoreNotEnough);
            }
            return true;
        }

        public async Task<bool> CheckIfCCFindexScoreIsEnough(int carId, int customerId)
        {
            var customer = await this.corporateCustomerRepository.GetAsync(i => i.Id == customerId);
            var car = await this._carRepository.GetAsync(c => c.Id == carId);
            var findexScore = await this._findexScoreService.getICFindexScore(customer.TaxNumber);

            if(findexScore < car.FindexScore)
            {
                throw new BusinessException(Messages.FindexScoreNotEnough);
            }
            return true;
        }        
    }

}
