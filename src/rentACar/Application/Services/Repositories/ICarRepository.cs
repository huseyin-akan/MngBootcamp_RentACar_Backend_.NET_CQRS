using Application.Features.Cars.Dtos;
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
    public interface ICarRepository : IAsyncRepository<Car>
    {
        Task<IPaginate<CarListDto>> GetAllCars(int index = 0, int size = 10, CancellationToken cancellationToken = default);
        Task<IPaginate<CarListDto>> GetAllRentableCars(int index = 0, int size = 10, CancellationToken cancellationToken = default);
        Task<IPaginate<CarListDto>> GetAllCarsByCity(int cityId, int index = 0, int size = 10, CancellationToken cancellationToken = default);
    }
}
