using Application.Features.Maintenenaces.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Models
{
    public class MaintenanceListModel : BasePagebleModel
    {
        public IList<MaintenanceListDto> Items { get; set; }
    }
}
