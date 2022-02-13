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
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IUserRepository _userRepository;

        public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository, IUserRepository userRepository)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _userRepository = userRepository;
        }

        public async Task CheckIfIndividualCustomerExists(int id)
        {
            var result = await _individualCustomerRepository.GetAsync(b => b.Id == id);

            if (result is null)
            {
                throw new BusinessException(Messages.IndividualCustomerDoesntExist);
            }
        }

        public async Task CheckIfNationalIdUsed(string nationalId)
        {
            var result = await _individualCustomerRepository.GetAsync(i => i.NationalId == nationalId);

            if(result is not null)
            {
                throw new BusinessException(Messages.NationalIdAlreadyUsed);
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
