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

namespace Application.Features.PromotionCodes.Commands.UpdatePromotionCode
{
    public class UpdatePromotionCodeCommand : IRequest<UpdatedPromotionCodeDto>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int DiscountRate { get; set; }
        public DateTime ValidityDate { get; set; }
        public class CreatePromotionCodeCommandHandler : IRequestHandler<UpdatePromotionCodeCommand, UpdatedPromotionCodeDto>
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

            public async Task<UpdatedPromotionCodeDto> Handle(UpdatePromotionCodeCommand request, CancellationToken cancellationToken)
            {
                await _promotionCodeBusinessRules.CheckIfPromotionCodeIsDuplicated(request.Code);

                var mappedProCode = _mapper.Map<PromotionCode>(request);

                var updatedCode = await _promotionCodeRepository.UpdateAsync(mappedProCode);
                var codeToReturn = _mapper.Map<UpdatedPromotionCodeDto>(updatedCode);
                return codeToReturn;
            }
        }
    }
}

