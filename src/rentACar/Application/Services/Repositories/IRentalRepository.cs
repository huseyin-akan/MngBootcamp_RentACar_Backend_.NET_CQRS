using Application.Features.Rentals.Dtos;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IRentalRepository : IAsyncRepository<Rental>
    {
        bool CheckIfCarIsRented(int carId);

        Task<IPaginate<ActiveRentalsListDto>> GetActiveRentals(int index = 0, int size = 10, CancellationToken cancellationToken = default);
    }
}
