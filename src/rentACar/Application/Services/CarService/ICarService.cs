using Application.Features.Cars.Commands.UpdateCar;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CarService
{
    public interface ICarService
    {
        Task<int> GetFindexScoreById(int id);
        Task<Car> GetCarById(int id);
        Task UpdateCarState(UpdateCarStateCommand command);
        Task UpdateCarAfterRentalEnd(UpdateCarAfterRentalEndCommand command);
    }
}
