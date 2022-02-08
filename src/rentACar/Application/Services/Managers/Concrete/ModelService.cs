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
    public class ModelService : IModelService
    {
        public IModelRepository modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            this.modelRepository = modelRepository;
        }

        public async Task<double> GetDailyPriceById(int modelId)
        {
            var result = await modelRepository.GetAsync(m => m.Id == modelId);

            if (result is null)
            {
                throw new RepositoryException(Messages.ModelNotFound);
            }
            return result.DailyPrice;
        }
    }
}
