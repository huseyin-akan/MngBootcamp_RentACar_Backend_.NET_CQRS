using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Rules
{
    public class FuelBusinessRules
    {
        readonly IFuelRepository _fuelRepository;

        public FuelBusinessRules(IFuelRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }

        //Gerkhin
        public async Task FuelNameCannotBeDuplicatedWhenInserted(string name)
        {
            var result = await _fuelRepository.GetListAsync(b => b.Name == name);

            if (result.Items.Any())
            {
                throw new BusinessException("Fuel name already exists");
            }
        }
    }
}
