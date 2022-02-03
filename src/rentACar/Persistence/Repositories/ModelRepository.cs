using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ModelRepository : EfRepositoryBase<Model, BaseDbContext>, IModelRepository
    {
        private BaseDbContext context;
        public ModelRepository(BaseDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Model>> GetAllModels()
        {
            using (context)
            {
                //var result1 = context.Words.Include(w => w.Meanings).Include(w => w.WrongAnswers).Include(w => w.Sentences).ThenInclude(s => s.Sentence).ToList();
                var result = context.Models.Include(c => c.Brand).Include(c => c.Transmission).Include(c => c.Fuel).ToListAsync();
                return await result;
            }
        }
    }
}
