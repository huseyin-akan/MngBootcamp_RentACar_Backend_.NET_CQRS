using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Utilities.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public  class CreateBrandCommand :IRequest<DataResult<Brand>>
    {
        public string Name {get; set;}

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, DataResult<Brand>>
        {
            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<DataResult<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //business rules
                await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);
                
                var mappedBrand =  _mapper.Map<Brand>(request);

                var createdBrand = await _brandRepository.AddAsync(mappedBrand);
                return new SuccessDataResult<Brand>(createdBrand);
            } 
        }
    }
}
