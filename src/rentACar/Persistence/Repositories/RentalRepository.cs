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
    }
}
