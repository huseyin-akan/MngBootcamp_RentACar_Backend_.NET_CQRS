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
    public class GetInvoiceListQuery : IRequest<InvoiceListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetInvoiceListQueryHandler : IRequestHandler<GetInvoiceListQuery, InvoiceListModel>
        {
            IInvoiceRepository _invoiceRepository;
            IMapper _mapper;

            public GetInvoiceListQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
            {
                _invoiceRepository = invoiceRepository;
                _mapper = mapper;
            }

            public async Task<InvoiceListModel> Handle(GetInvoiceListQuery request, CancellationToken cancellationToken)
            {
                var invoicelistModel = await _invoiceRepository.GetAllInvoices(
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedInvoices = _mapper.Map<InvoiceListModel>(invoicelistModel);
                return mappedInvoices;
            }
        }
    }
}
