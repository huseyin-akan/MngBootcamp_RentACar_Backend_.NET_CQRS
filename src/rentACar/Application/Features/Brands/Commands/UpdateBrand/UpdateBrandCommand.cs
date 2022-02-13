using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand :IRequest<UpdateBrandDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateBrandCommandHandler: IRequestHandler<UpdateBrandCommand, UpdateBrandDto>
        {
            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;
            ICacheService _cacheService;

            public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
            BrandBusinessRules brandBusinessRules, ICacheService cacheService)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
                _cacheService = cacheService;
            }

            public async Task<UpdateBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

                var mappedBrand = _mapper.Map<Brand>(request);
                var updatedBrand = await _brandRepository.UpdateAsync(mappedBrand);
                var brandToReturn = _mapper.Map<UpdateBrandDto>(updatedBrand);

                _cacheService.Remove("brands-list");

                return brandToReturn;
            }
        }
        
    }
}
