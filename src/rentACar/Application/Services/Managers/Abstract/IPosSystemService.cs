using Application.Features.CreditCardInfos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Managers.Abstract
{
    public interface IPosSystemService
    {
        Task<bool> MakePayment(CreateCreditCardInfosDto creditCardInfos);
    }
}
