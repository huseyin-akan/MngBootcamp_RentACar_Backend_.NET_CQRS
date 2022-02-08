using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CreditCardInfos.Dtos
{
    public class CreateCreditCardInfosDto
    {
        public string CreditCardNo { get; set; }
        public string ValidDate { get; set; }
        public string CVC { get; set; }
        public string CardHolder { get; set; }
    }
}
