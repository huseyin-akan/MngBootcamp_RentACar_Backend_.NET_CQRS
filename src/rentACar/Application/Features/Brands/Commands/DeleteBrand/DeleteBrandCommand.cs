using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<DeleteBrandDto>, ILoggableRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandDto>
        {
            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;
            ICacheService _cacheService;

            public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                BrandBusinessRules brandBusinessRules, ICacheService cacheService)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
                _cacheService = cacheService;
            }

            public async Task<DeleteBrandDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
            {
                var brandToDelete = await _brandRepository.GetAsync(b => b.Id == request.Id);

                var deletedBrand = await _brandRepository.DeleteAsync(brandToDelete);

                _cacheService.Remove("brands-list");

                var brandToReturn = _mapper.Map<DeleteBrandDto>(deletedBrand);

                return brandToReturn;
            }
        }
    }
}
