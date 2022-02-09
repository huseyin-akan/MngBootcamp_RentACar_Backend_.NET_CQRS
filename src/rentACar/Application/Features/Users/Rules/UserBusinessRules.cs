using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}
