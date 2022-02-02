using Application.Features.Brands.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Models
{
    public class BrandListModel : BasePagebleModel
    {
        public IList<BrandListDto> Items { get; set; }
    }
}
