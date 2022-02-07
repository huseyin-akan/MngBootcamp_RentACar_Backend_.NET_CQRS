using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CreditCardInfo : Entity
    {
        public int CustomerId { get; set; }
        public string CreditCardNo { get; set; }
        public string ValidDate { get; set; }
        public string CVC { get; set; }
        public string CardHolder { get; set; }

        public Customer Customer { get; set; }

        public CreditCardInfo()
        {

        }

        public CreditCardInfo(int id, int customerId, string creditCardNo,
            string validDate, string cVC, string cardHolder)
        {
            Id = id;
            CustomerId = customerId;
            CreditCardNo = creditCardNo;
            ValidDate = validDate;
            CVC = cVC;
            CardHolder = cardHolder;
        }
    }
}
