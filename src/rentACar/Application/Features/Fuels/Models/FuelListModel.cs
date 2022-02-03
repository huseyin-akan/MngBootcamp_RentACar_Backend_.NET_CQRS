using Application.Features.Fuels.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Models
{
    public class FuelListModel : BasePagebleModel
    {
        public IList<FuelListDto> Items { get; set; }
    }
}
