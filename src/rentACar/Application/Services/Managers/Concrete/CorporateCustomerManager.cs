using Application.Services.Managers.Abstract;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Concrete
{
    public class CorporateCustomerManager : ICorporateCustomerService
    {
        readonly ICorporateCustomerRepository corporateCustomerRepository;

        public CorporateCustomerManager(ICorporateCustomerRepository corporateCustomerRepository)
        {
            this.corporateCustomerRepository = corporateCustomerRepository;
        }

        public async Task<string> GetTaxNumber(int id)
        {
            var customer = await this.corporateCustomerRepository.GetAsync(c => c.Id == id);
            if(customer is null)
            {
                throw new RepositoryException(Messages.CorporateCustomerDoesntExist);
            }
            return customer.TaxNumber;
        }
    }
}
