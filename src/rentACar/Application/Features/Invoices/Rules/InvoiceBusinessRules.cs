using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Rules
{
    public class InvoiceBusinessRules
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceBusinessRules(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

    }
}
