using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Services.Managers.Abstract;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Concrete
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _carRepository;
        private readonly IMediator _mediator;

        public InvoiceService(IInvoiceRepository carRepository, IMediator mediator)
        {
            _carRepository = carRepository;
            _mediator = mediator;
        }

        public async Task MakeOutInvoice(CreateInvoiceCommand command)
        {
            var result = await this._mediator.Send(command);

            if (result is null)
            {
                throw new BusinessException(Messages.InvoiceNotAdded);
            }
        }
    }
}