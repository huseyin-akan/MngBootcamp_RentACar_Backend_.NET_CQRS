using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Rules
{
    public class ModelBusinessRules
    {
        readonly IModelRepository _modelRepository;

        public ModelBusinessRules(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task ModelNameCannotBeDuplicatedWhenInserted(string name)
        {
            var result = await _modelRepository.GetListAsync(b => b.Name == name);

            if (result.Items.Any())
            {
                throw new BusinessException("Model name already exists");
            }
        }
    }

        
}
