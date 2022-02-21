using Application.Features.PromotionCodes.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PromotionCodes.Models
{
    public class PromotionCodeListModel : BasePagebleModel
    {
        public IList<PromotionCodeListDto> Items { get; set; }
    }
}
