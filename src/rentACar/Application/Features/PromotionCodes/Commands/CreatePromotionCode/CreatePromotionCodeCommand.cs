using Application.Features.PromotionCodes.Dtos;
using Application.Features.PromotionCodes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Commands.CreatePromotionCode
{
    public class CreatePromotionCodeCommand : IRequest<CreatedPromotionCodeDto>
    {
        public string Code { get; set; }
        public int DiscountRate { get; set; }
        public DateTime ValidityDate { get; set; }
        public class CreatePromotionCodeCommandHandler : IRequestHandler<CreatePromotionCodeCommand, CreatedPromotionCodeDto>
        {
            IPromotionCodeRepository _promotionCodeRepository;
            IMapper _mapper;
            PromotionCodeBusinessRules _promotionCodeBusinessRules;

            public CreatePromotionCodeCommandHandler(IPromotionCodeRepository promotionCodeRepository, IMapper mapper, PromotionCodeBusinessRules promotionCodeBusinessRules)
            {
                _promotionCodeRepository = promotionCodeRepository;
                _mapper = mapper;
                _promotionCodeBusinessRules = promotionCodeBusinessRules;
            }

            public async Task<CreatedPromotionCodeDto> Handle(CreatePromotionCodeCommand request, CancellationToken cancellationToken)
            {
                await _promotionCodeBusinessRules.CheckIfPromotionCodeIsDuplicated(request.Code);

                var mappedProCode = _mapper.Map<PromotionCode>(request);

                var createdCode = await _promotionCodeRepository.AddAsync(mappedProCode);
                var codeToReturn = _mapper.Map<CreatedPromotionCodeDto>(createdCode);
                return codeToReturn;
            }
        }
    }
}
