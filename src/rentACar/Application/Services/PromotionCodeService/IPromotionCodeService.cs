using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PromotionCodeService
{
    public interface IPromotionCodeService
    {
        Task<int> GetPromotionCodeDiscount(string code);
    }
}
