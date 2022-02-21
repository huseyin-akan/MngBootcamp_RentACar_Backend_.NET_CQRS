using Application.Features.PromotionCodes.Dtos;
using Application.Features.PromotionCodes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Queries.GetPromotionCode
{
    public class GetPromotionCodeQuery : IRequest<PromotionCodeListDto>
    {
        public string Code { get; set; }

        public int CustomerId { get; set; }

        public class GetColorListHandler : IRequestHandler<GetPromotionCodeQuery, PromotionCodeListDto>
        {
            IPromotionCodeRepository _promotionCodeRepository;
            IMapper _mapper;
            PromotionCodeBusinessRules _promotionCodeBusinessRules;

            public GetColorListHandler(IPromotionCodeRepository promotionCodeRepository, IMapper mapper, PromotionCodeBusinessRules promotionCodeBusinessRules)
            {
                _promotionCodeRepository = promotionCodeRepository;
                _mapper = mapper;
                _promotionCodeBusinessRules = promotionCodeBusinessRules;
            }

            public async Task<PromotionCodeListDto> Handle(GetPromotionCodeQuery request, CancellationToken cancellationToken)
            {
                var code = await _promotionCodeRepository.GetAsync(pc => pc.Code == request.Code );
                if (code is null) throw new BusinessException(Messages.ProCodeNotFound);

                await this._promotionCodeBusinessRules.CheckIfPromotionCodeIsUsed(request.Code, request.CustomerId);

                var mappedCode = _mapper.Map<PromotionCodeListDto>(code);

                return mappedCode;
            }
        }
    }
}
