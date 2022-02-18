using Application.Features.Models.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Paging;
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
        public ModelRepository(BaseDbContext context) : base(context)
        {

        }

        public async Task<IPaginate<ModelListDto>> GetAllModels(
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            
                var result = from m in Context.Models
                             join b in Context.Brands
                             on m.BrandId equals b.Id
                             join t in Context.Transmissions
                             on m.TransmissionId equals t.Id
                             join f in Context.Fuels
                             on m.FuelId equals f.Id
                             select new ModelListDto
                             {
                                 Id = m.Id,
                                 ImageUrl = m.ImageUrl,
                                 DailyPrice = m.DailyPrice,
                                 Name = m.Name,
                                 FuelId = m.FuelId,
                                 Fuel = f.Name,
                                 BrandId = m.BrandId,
                                 Brand = b.Name,
                                 TransmissionId = m.Id,
                                 Transmission = t.Name
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);
            
        }

        //TODO: burada brand, transmission ve fuel elemanlarının virtual alanları da geliyor. Yukarıdaki çok daha iyi
        public async Task<IPaginate<Model>> GetAllModels2(
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            
                var result = Context.Models
                    .Include(c => c.Brand)
                    .Include(c => c.Transmission)
                    .Include(c => c.Fuel)
                    .ToPaginateAsync(index, size, 0, cancellationToken);
                return await result;
            
        }
    }
}
