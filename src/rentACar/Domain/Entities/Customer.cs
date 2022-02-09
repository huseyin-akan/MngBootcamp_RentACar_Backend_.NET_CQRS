using Core.Persistence.Repositories;
using Core.Security.Entities;
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

        public Customer()
        {
            Rentals = new HashSet<Rental>();
        }

        public Customer(int id, string email) : this()
        {

        }
    }
}
