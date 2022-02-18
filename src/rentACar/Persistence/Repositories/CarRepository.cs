using Application.Features.Cars.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CarRepository : EfRepositoryBase<Car, BaseDbContext>, ICarRepository
    {
        public CarRepository(BaseDbContext context) : base(context)
        {
            
        }
        public async Task<IPaginate<CarListDto>> GetAllCars(
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {            
                var result = from c in Context.Cars
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join co in Context.Colors
                             on c.ColorId equals co.Id
                             join ci in Context.Cities
                             on c.CityId equals ci.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id

                             select new CarListDto
                             {
                                 Id = c.Id,
                                 Model = m.Name,
                                 ModelId = c.ModelId,
                                 Color = co.Name,
                                 ColorId = c.ColorId,
                                 City = ci.Name,
                                 CityId = c.CityId,
                                 Plate = c.Plate,
                                 ModelYear = c.ModelYear,
                                 FindexScore = c.FindexScore,
                                 Kilometer = c.Kilometer,
                                 CarState = c.CarState,
                                 DailyPrice = m.DailyPrice,
                                 Brand = b.Name,
                                 ImageUrl = m.ImageUrl
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);            
        }

        public async Task<IPaginate<CarListDto>> GetAllRentableCars(
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            
                var result = from c in Context.Cars
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join co in Context.Colors
                             on c.ColorId equals co.Id
                             join ci in Context.Cities
                             on c.CityId equals ci.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id
                             where c.CarState == CarState.Available
                             select new CarListDto
                             {
                                 Id = c.Id,
                                 Model = m.Name,
                                 Color = co.Name,
                                 City = ci.Name,
                                 ModelYear = c.ModelYear,
                                 CarState = c.CarState,
                                 DailyPrice = m.DailyPrice,
                                 Brand = b.Name,
                                 ImageUrl = m.ImageUrl
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);            
        }

        public async Task<IPaginate<CarListDto>> GetAllCarsByCity(
            int cityId,
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            
                var result = from c in Context.Cars
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join co in Context.Colors
                             on c.ColorId equals co.Id
                             join ci in Context.Cities
                             on c.CityId equals ci.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id
                             where c.CityId == cityId
                             select new CarListDto
                             {
                                 Id = c.Id,
                                 Model = m.Name,
                                 Color = co.Name,
                                 City = ci.Name,
                                 ModelYear = c.ModelYear,
                                 CarState = c.CarState,
                                 DailyPrice = m.DailyPrice,
                                 Brand = b.Name,
                                 ImageUrl = m.ImageUrl
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);
            
        }

    }
}
