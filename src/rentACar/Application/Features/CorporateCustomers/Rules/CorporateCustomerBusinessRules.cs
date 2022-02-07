using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Rules
{
    public class CorporateCustomerBusinessRules
    {
        readonly ICorporateCustomerRepository _corporateCustomerRepository;

        public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
        }

        public async Task CheckIfCorporateCustomerExists(int id)
        {
            var result = await _corporateCustomerRepository.GetAsync(b => b.Id == id);

            if (result is null)
            {
                throw new BusinessException(Messages.CorporateCustomerDoesntExist);
            }
        }
    }
}
