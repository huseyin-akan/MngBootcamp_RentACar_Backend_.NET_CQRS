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
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IUserRepository _userRepository;

        public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository, IUserRepository userRepository)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _userRepository = userRepository;
        }

        public async Task CheckIfCorporateCustomerExists(int id)
        {
            var result = await _corporateCustomerRepository.GetAsync(b => b.Id == id);

            if (result is null)
            {
                throw new BusinessException(Messages.CorporateCustomerDoesntExist);
            }
        }

        public async Task CheckIfTaxNumberUsed(string taxNumber)
        {
            var result = await _corporateCustomerRepository.GetAsync(i => i.TaxNumber == taxNumber);

            if (result is not null)
            {
                throw new BusinessException(Messages.TaxNumberAlreadyUsed);
            }
        }

        public async Task CheckIfUserNameTaken(string userName)
        {
            var result = await _userRepository.GetAsync(u => u.UserName == userName);

            if (result is not null)
            {
                throw new BusinessException(Messages.UsernameAlreadyTaken);
            }
        }

        public async Task CheckIfEmailTaken(string email)
        {
            var result = await _userRepository.GetAsync(u => u.Email == email);

            if (result is not null)
            {
                throw new BusinessException(Messages.EmailAlreadyTaken);
            }
        }
    }
}
