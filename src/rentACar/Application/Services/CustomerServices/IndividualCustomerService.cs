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
    public class IndividualCustomerService : IIndividualCustomerService
    {
        readonly IIndividualCustomerRepository individualCustomerRepository;

        public IndividualCustomerService(IIndividualCustomerRepository individualCustomerRepository)
        {
            this.individualCustomerRepository = individualCustomerRepository;
        }

        public async Task<string> GetNationalId(int id)
        {
            var customer = await this.individualCustomerRepository.GetAsync(c => c.Id == id);
            if (customer is null)
            {
                throw new RepositoryException(Messages.IndividualCustomerDoesntExist);
            }
            return customer.NationalId;
        }
    }
}
