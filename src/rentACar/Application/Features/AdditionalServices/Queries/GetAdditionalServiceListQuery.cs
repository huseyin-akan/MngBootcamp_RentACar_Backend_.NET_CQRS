using Application.Features.AdditionalServices.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Queries
{
    public class GetAdditionalServiceListQuery : IRequest<AdditionalServiceListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetAdditionalServiceListHandler : IRequestHandler<GetAdditionalServiceListQuery, AdditionalServiceListModel>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;

            public GetAdditionalServiceListHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
            }

            public async Task<AdditionalServiceListModel> Handle(GetAdditionalServiceListQuery request, CancellationToken cancellationToken)
            {
                var additionalServices = await _additionalServiceRepository.GetListAsync(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedAdditionalServices = _mapper.Map<AdditionalServiceListModel>(additionalServices);
                return mappedAdditionalServices;
            }
        }
    }
}
