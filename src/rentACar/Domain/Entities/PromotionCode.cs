using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PromotionCode :Entity
    {
        public string Code { get; set; }
        public int DiscountRate { get; set; }
        public DateTime ValidityDate { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public PromotionCode()
        {
            this.Customers = new HashSet<Customer>();
            this.Code = "";
        }

        public PromotionCode(int id, string code, int discountRate, DateTime validityDate) :this()
        {
            Id = id;
            Code = code;
            DiscountRate = discountRate;
            ValidityDate = validityDate;
        }
    }
}
