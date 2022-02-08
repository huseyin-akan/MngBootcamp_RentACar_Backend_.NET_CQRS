using Application.Features.Invoices.Dtos;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface IInvoiceRepository : IAsyncRepository<Invoice>
    {
        Task<IPaginate<InvoiceListDto>> GetAllInvoices(int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default);

        Task<IPaginate<InvoiceListDto>> GetAllInvoicesBetweenDates(
            DateTime startDate,
            DateTime endDate,
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default
            );
        Task<IPaginate<InvoiceListDto>> GetAllInvoicesByCustomerId(
            int customerId,
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default
            );
    }
}
