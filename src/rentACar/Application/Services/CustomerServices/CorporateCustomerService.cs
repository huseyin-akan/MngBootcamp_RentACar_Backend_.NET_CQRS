using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CustomerServices
{
    public class CorporateCustomerService : ICorporateCustomerService
    {
        readonly ICorporateCustomerRepository corporateCustomerRepository;

        public CorporateCustomerService(ICorporateCustomerRepository corporateCustomerRepository)
        {
            this.corporateCustomerRepository = corporateCustomerRepository;
        }

        public async Task<string> GetTaxNumber(int id)
        {
            var customer = await this.corporateCustomerRepository.GetAsync(c => c.Id == id);
            if(customer is null)
            {
                throw new BusinessException(Messages.CorporateCustomerDoesntExist);
            }
            return customer.TaxNumber;
        }
    }
}
