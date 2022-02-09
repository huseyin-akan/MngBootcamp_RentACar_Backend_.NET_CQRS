using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<CreateInvoiceDto> MakeOutInvoice(CreateInvoiceCommand command);
    }
}
