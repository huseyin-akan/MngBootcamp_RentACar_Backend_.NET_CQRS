using Application.Features.CreditCardInfos.Dtos;
using Application.Services.PosSystemService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Rules
{
    public class PaymentBusinessRules
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPosSystemService posSystemService;

        public PaymentBusinessRules(IPaymentRepository paymentRepository, IPosSystemService posSystemService)
        {
            _paymentRepository = paymentRepository;
            this.posSystemService = posSystemService;
        }

        public async Task CheckIfPaymentIsSuccessful(CreateCreditCardInfosDto creditCardInfos)
        {
            var result = await posSystemService.MakePayment(creditCardInfos);

            if (!result)
            {
                throw new BusinessException(Messages.PaymentFailed);
            }
        }
    }
}
