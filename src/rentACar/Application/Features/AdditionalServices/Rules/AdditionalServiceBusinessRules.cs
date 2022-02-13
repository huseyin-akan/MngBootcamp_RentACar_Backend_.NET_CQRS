using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Rules
{
    public class AdditionalServiceBusinessRules
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;

        public AdditionalServiceBusinessRules(IAdditionalServiceRepository additionalServiceRepository)
        {
            _additionalServiceRepository = additionalServiceRepository;
        }

        public async Task AdditionalServiceNameCannotBeDuplicated(string serviceName)
        {
            var result = await _additionalServiceRepository.GetAsync(b => b.ServiceName == serviceName);

            if (result is not null)
            {
                throw new BusinessException("Additional service name already exists");
            }
        }
    }
}
