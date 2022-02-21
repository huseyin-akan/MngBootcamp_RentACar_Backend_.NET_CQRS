using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PromotionCodeService
{
    public class PromotionCodeService : IPromotionCodeService
    {
        private readonly IPromotionCodeRepository _promotionCodeRepository;

        public PromotionCodeService(IPromotionCodeRepository promotionCodeRepository)
        {
            _promotionCodeRepository = promotionCodeRepository;
        }

        public async Task<int> GetPromotionCodeDiscount(string code)
        {
            var result = await this._promotionCodeRepository.GetAsync(pc => pc.Code == code);
            if (result is null) throw new BusinessException("Böyle bir promosyon kodu bulunmamaktadır.");
            return result.DiscountRate;
        }
    }
}
