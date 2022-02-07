using Application.Services.Managers.Abstract;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Concrete
{
    public class CarManager : ICarService
    {
        readonly ICarRepository _carRepository;

        public CarManager(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<int> GetFindexScoreById(int id)
        {
            var result = await _carRepository.GetAsync(c => c.Id == id);
            if(result is null)
            {
                throw new RepositoryException(Messages.CarNotFound);
            }
            return result.FindexScore;
        }
    }
}
