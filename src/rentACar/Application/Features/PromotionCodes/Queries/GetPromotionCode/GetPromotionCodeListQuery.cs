using Application.Features.PromotionCodes.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Queries.GetPromotionCode
{

    public class GetPromotionCodeListQuery : IRequest<PromotionCodeListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetColorListHandler : IRequestHandler<GetPromotionCodeListQuery, PromotionCodeListModel>
        {
            IPromotionCodeRepository _promotionCodeRepository;
            IMapper _mapper;

            public GetColorListHandler(IPromotionCodeRepository promotionCodeRepository, IMapper mapper)
            {
                _promotionCodeRepository = promotionCodeRepository;
                _mapper = mapper;
            }

            public async Task<PromotionCodeListModel> Handle(GetPromotionCodeListQuery request, CancellationToken cancellationToken)
            {
                var codes = await _promotionCodeRepository.GetListAsync(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCodes= _mapper.Map<PromotionCodeListModel>(codes);
                return mappedCodes;
            }
        }
    }
}
