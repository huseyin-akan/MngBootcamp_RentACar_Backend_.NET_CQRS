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
    public class PromotionCodeRepository : EfRepositoryBase<PromotionCode, BaseDbContext>, IPromotionCodeRepository
    {
        public PromotionCodeRepository(BaseDbContext context) : base(context)
        {

        }

        public Task UsePromotionCodeForCustomer(string code, int customerId)
        {
            var codeEntity = Context.PromotionCodes.Where(p => p.Code == code).FirstOrDefault();
            var customerToAdd = Context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            if(codeEntity is not null && customerToAdd is not null)
            {
                codeEntity.Customers.Add(customerToAdd);
            }
            Context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
