using Application.Features.Cities.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cities.Models
{
    public class CityListModel : BasePagebleModel
    {
        public IList<CityListDto> Items { get; set; }
    }
}
