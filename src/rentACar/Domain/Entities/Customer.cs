using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer :User
    {
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<PromotionCode> PromotionCodes { get; set; }

        public Customer()
        {
            Rentals = new HashSet<Rental>();
            PromotionCodes = new HashSet<PromotionCode>();
        }

        public Customer(int id, string email) : this()
        {

        }
    }
}
