using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaims(User user);
        Task<User> GetByMail(string email);
    }
}
