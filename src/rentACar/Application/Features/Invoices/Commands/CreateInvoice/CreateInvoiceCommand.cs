using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<CreateInvoiceDto>
    {
        public long InvoiceNo { get; set; }
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalSum { get; set; }

        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceDto>
        {
            IInvoiceRepository _invoiceRepository;
            IMapper _mapper;
            InvoiceBusinessRules _invoiceBusinessRules;

            public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository,
                IMapper mapper,
                InvoiceBusinessRules invoiceBusinessRules)
            {
                _invoiceRepository = invoiceRepository;
                _mapper = mapper;
                _invoiceBusinessRules = invoiceBusinessRules;
            }

            public async Task<CreateInvoiceDto> Handle(CreateInvoiceCommand request,
                CancellationToken cancellationToken)
            {
                var mappedInvoice = _mapper.Map<Invoice>(request);

                var createdInvoice = await _invoiceRepository.AddAsync(mappedInvoice);

                var invoiceToReturn = await _invoiceRepository.GetInvoiceDetailsById(createdInvoice.Id);

                return invoiceToReturn;
            }
        }
    }
}
