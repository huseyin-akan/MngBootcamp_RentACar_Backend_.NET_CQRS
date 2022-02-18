using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        readonly IBrandRepository  _brandRepository;

        public BrandBusinessRules(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        //Gerkhin
        public async Task BrandNameCannotBeDuplicatedWhenInserted(string name)
        {
            var result = await _brandRepository.GetListAsync( b => b.Name == name);
            
            if(result.Items.Any() )
            {
                throw new BusinessException(Messages.BrandNameExists);
            }
        } 

        public async Task CheckIfBrandExists(int id)
        {
            var result = await _brandRepository.GetAsync(b => b.Id == id);

            if(result is null)
            {
                throw new BusinessException(Messages.BrandNotFound);
            }
        }
    }
}
