using Application.Features.Invoices.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Queries.GetInvoice
{
    public class GetInvoiceListBetweenDatesQuery : IRequest<InvoiceListModel>
    {
        public PageRequest PageRequest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetInvoiceListBetweenDatesQueryHandler : IRequestHandler<GetInvoiceListBetweenDatesQuery, InvoiceListModel>
        {
            IInvoiceRepository _invoiceRepository;
            IMapper _mapper;

            public GetInvoiceListBetweenDatesQueryHandler(IInvoiceRepository invoiceRepository,
                IMapper mapper)
            {
                _invoiceRepository = invoiceRepository;
                _mapper = mapper;
            }

            public async Task<InvoiceListModel> Handle(GetInvoiceListBetweenDatesQuery request,
                CancellationToken cancellationToken)
            {
                var invoicelistModel = await _invoiceRepository.GetAllInvoicesBetweenDates(
                    startDate: request.StartDate,
                    endDate: request.EndDate,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedInvoices = _mapper.Map<InvoiceListModel>(invoicelistModel);
                return mappedInvoices;
            }
        }
    }
}
