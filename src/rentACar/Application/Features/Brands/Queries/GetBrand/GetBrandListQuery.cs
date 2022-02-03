using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetBrand
{
    public class GetBrandListQuery : IRequest<DataResult<BrandListModel>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetBrandListHandler : IRequestHandler<GetBrandListQuery, DataResult<BrandListModel>>
        {
            IBrandRepository _brandRepository;
            IMapper _mapper;

            public GetBrandListHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<DataResult<BrandListModel>> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
            {
                var brands = await _brandRepository.GetListAsync(
                    index:request.PageRequest.Page,
                    size : request.PageRequest.PageSize
                    );
                var mappedBrands = _mapper.Map<BrandListModel>(brands);
                return new SuccessDataResult<BrandListModel>(mappedBrands);
            }
        }
    }
}
