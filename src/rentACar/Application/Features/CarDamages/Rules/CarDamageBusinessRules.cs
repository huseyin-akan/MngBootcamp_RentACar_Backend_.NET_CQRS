using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Rules
{
    public class CarDamageBusinessRules
    {
        private readonly ICarDamageRepository _carDamageRepository;

        public CarDamageBusinessRules(ICarDamageRepository carDamageRepository)
        {
            _carDamageRepository = carDamageRepository;
        }

    }
}
