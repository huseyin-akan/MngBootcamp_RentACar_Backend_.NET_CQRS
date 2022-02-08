using Application.Features.Invoices.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Models
{
    public class InvoiceListModel : BasePagebleModel
    {
        public IList<InvoiceListDto> Items { get; set; }
    }
}
