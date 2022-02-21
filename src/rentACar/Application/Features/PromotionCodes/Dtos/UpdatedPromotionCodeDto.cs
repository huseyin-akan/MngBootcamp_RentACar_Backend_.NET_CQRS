using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Dtos
{
    public class UpdatedPromotionCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int DiscountRate { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}
