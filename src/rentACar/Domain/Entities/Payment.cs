using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment :Entity
    {
        public DateTime PaymentDate { get; set; }
        public double TotalSum { get; set; }

        public int RentalId { get; set; }

        public Rental Rental { get; set; }

        public Payment()
        {

        }

        public Payment(int id, DateTime paymentDate, double totalSum, int rentalId)
        {
            Id = id;
            PaymentDate = paymentDate;
            TotalSum = totalSum;
            RentalId = rentalId;
        }
    }
}
