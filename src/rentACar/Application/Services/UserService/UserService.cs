using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetByMail(string email)
        {
            var result = await this.userRepository.GetAsync(u => u.Email == email);
            if(result is null)
            {
                throw new RepositoryException(Messages.UserNotFound);
            }

            return result;
        }

        public Task<List<OperationClaim>> GetClaims(User user)
        {
            var result = Task.Run(() =>
            {
                var claims = this.userRepository.GetClaims(user);

                if (claims is null)
                {
                    throw new RepositoryException(Messages.UserNotFound);
                }

                return claims;
            });

            return result;
            
        }
    }
}
