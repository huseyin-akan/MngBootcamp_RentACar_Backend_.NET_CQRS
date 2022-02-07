using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Rules
{
    public class IndividualCustomerBusinessRules
    {
        readonly IIndividualCustomerRepository _individualCustomerRepository;

        public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository)
        {
            _individualCustomerRepository = individualCustomerRepository;
        }

        public async Task CheckIfIndividualCustomerExists(int id)
        {
            var result = await _individualCustomerRepository.GetAsync(b => b.Id == id);

            if (result is null)
            {
                throw new BusinessException(Messages.IndividualCustomerDoesntExist);
            }
        }
    }

}
