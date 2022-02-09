﻿using Application.Features.Rentals.Commands.RentForCorporateCustomer;
using Application.Features.Rentals.Commands.RentForIndividualCustomer;
using Application.Services.ModelService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Helpers
{
    public static class TotalSumCalculator
    {
        public static async Task<double> CalculateTotalSumForCC(RentForCorporateCustomerCommand request, Car car, IModelService modelService)
        {
            var totalDays = (request.ReturnDate.Date - request.RentDate.Date).Days + 1;

            var dailyPrice = await modelService.GetDailyPriceById(car.ModelId);
            var totalSum = dailyPrice * totalDays;

            bool differentCities = request.RentCityId != request.ReturnCityId;
            if (differentCities)
            {
                totalSum += 500;
            }
            return totalSum;
        }

        public static async Task<double> CalculateTotalSumForIC(RentForIndividualCustomerCommand request, Car car, IModelService modelService)
        {
            var totalDays = (request.ReturnDate.Date - request.RentDate.Date).Days + 1;

            var dailyPrice = await modelService.GetDailyPriceById(car.ModelId);
            var totalSum = dailyPrice * totalDays;

            bool differentCities = request.RentCityId != request.ReturnCityId;
            if (differentCities)
            {
                totalSum += 500;
            }
            return totalSum;
        }
    }
}