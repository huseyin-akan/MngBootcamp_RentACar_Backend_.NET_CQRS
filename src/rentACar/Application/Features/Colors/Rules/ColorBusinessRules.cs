using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Rules
{
    public class ColorBusinessRules
    {
        private readonly IColorRepository _colorRepository;

        public ColorBusinessRules(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task ColorNameCannotBeDuplicatedWhenInserted(string name)
        {
            var result = await _colorRepository.GetListAsync(b => b.Name == name);

            if (result.Items.Any())
            {
                throw new BusinessException("Color name already exists");
            }
        }
    }
}
