using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, BaseDbContext>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in Context.OperationClaims
                         join userOperationClaim in Context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { 
                             Id = operationClaim.Id,
                             Name = operationClaim.Name };
            return result.ToList();
        }
    }
}
