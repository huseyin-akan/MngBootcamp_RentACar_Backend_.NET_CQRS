using Application.Features.Colors.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Models
{
    public class ColorListModel : BasePagebleModel
    {
        public IList<ColorListDto> Items { get; set; }
    }
}
