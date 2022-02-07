﻿using Application.Features.CreditCardInfos.Dtos;
using Application.Services.Managers.Abstract;
using Core.Application.FakeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Concrete
{
    internal class FakePosSystemServiceAdapter : IPosSystemService
    {
        FakePosSystemService posService = new FakePosSystemService();
        public Task<bool> MakePayment(CreditCardInfosDto creditCardInfos)
        {
            Task<bool> result = Task.Run(() =>
            {
                return posService.GetPayment(creditCardInfos.CreditCardNo, creditCardInfos.CardHolder,
                    creditCardInfos.CVC, creditCardInfos.ValidDate);
            });
            return result;
        }
    }
}