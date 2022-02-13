using Application.Features.AdditionalServices.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Models
{
    public class AdditionalServiceListModel : BasePagebleModel
    {
        public IList<AdditionalServiceListDto> Items { get; set; }
    }
}
