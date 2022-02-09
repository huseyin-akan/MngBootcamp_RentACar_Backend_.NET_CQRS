using Application.Features.Payments.Commands.CreatePayment;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentService
{
    public class PaymentService :IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMediator mediator;

        public PaymentService(IPaymentRepository paymentRepository, IMediator mediator)
        {
            this.paymentRepository = paymentRepository;
            this.mediator = mediator;
        }

        public async Task MakePayment(CreatePaymentCommand command)
        {
            var result = await mediator.Send(command);

            if(result is null)
            {
                throw new BusinessException(Messages.PaymentFailed);
            }
        }
    }
}
