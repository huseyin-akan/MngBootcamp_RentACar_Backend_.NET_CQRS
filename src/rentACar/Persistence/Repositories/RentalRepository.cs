using Application.Features.Rentals.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Paging;
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
    public class RentalRepository : EfRepositoryBase<Rental, BaseDbContext>, IRentalRepository
    {
        public RentalRepository(BaseDbContext context) : base(context)
        {
        }

        public bool CheckIfCarIsRented(int carId)
        {
            var result = Context.Rentals.Where(r => r.ReturnedDate == null && r.CarId == carId).FirstOrDefault();
            if (result is null)
            {
                return false;
            }
            return true;
        }

        public async Task<IPaginate<ActiveRentalsListDto>> GetActiveRentals(
           int index = 0,
           int size = 10,
           CancellationToken cancellationToken = default)
        {
            var result = from r in Context.Rentals
                         join ci in Context.Cities
                         on r.RentCityId equals ci.Id
                         join ci2 in Context.Cities
                         on r.ReturnCityId equals ci2.Id
                         join c in Context.Cars
                         on r.CarId equals c.Id
                         join m in Context.Models
                         on c.ModelId equals m.Id
                         where r.ReturnedDate == null
                            
                         select new ActiveRentalsListDto
                         {
                            Id = r.Id,
                            RentDate = r.RentDate,
                            ReturnDate = r.ReturnDate,
                            RentCityId = r.RentCityId,
                            RentCity = ci.Name,
                            ReturnCityId = r.ReturnCityId,
                            ReturnCity = ci2.Name,
                            RentedKilometer = r.RentedKilometer,
                            CarId = r.CarId,
                            ModelName = m.Name,
                            ModelYear = c.ModelYear,
                            CustomerId = r.CustomerId
                         };

            return await result.ToPaginateAsync(index, size, 0, cancellationToken);
        }
    }

}
