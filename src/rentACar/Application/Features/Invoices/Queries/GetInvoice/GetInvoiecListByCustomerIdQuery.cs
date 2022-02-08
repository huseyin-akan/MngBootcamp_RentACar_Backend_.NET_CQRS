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
    public class GetInvoiecListByCustomerIdQuery : IRequest<InvoiceListModel>
    {
        public PageRequest PageRequest { get; set; }
        public int CustomerId { get; set; }

        public class GetInvoiecListByCustomerIdQueryHandler : IRequestHandler<GetInvoiecListByCustomerIdQuery, InvoiceListModel>
        {
            IInvoiceRepository _invoiceRepository;
            IMapper _mapper;

            public GetInvoiecListByCustomerIdQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
            {
                _invoiceRepository = invoiceRepository;
                _mapper = mapper;
            }

            public async Task<InvoiceListModel> Handle(GetInvoiecListByCustomerIdQuery request,
                CancellationToken cancellationToken)
            {
                var invoicelistModel = await _invoiceRepository.GetAllInvoicesByCustomerId(
                    customerId : request.CustomerId,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedInvoices = _mapper.Map<InvoiceListModel>(invoicelistModel);
                return mappedInvoices;
            }
        }
    }
}
