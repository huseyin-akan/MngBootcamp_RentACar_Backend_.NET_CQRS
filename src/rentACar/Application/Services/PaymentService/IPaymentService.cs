﻿using Application.Features.Payments.Commands.CreatePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentService
{
    public interface IPaymentService
    {
        Task MakePayment(CreatePaymentCommand command);
    }
}
