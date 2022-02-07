using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice : Entity
    {
        public long InvoiceNo { get; set; }
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalSum { get; set; }

        public Rental Rental { get; set; }
        public Customer Customer { get; set; }

        public Invoice()
        {

        }

        public Invoice(int id, long invoiceNo, int rentalId, int customerId, DateTime invoiceDate, double totalSum)
        {
            Id = id;
            InvoiceNo = invoiceNo;
            RentalId = rentalId;
            CustomerId = customerId;
            InvoiceDate = invoiceDate;
            TotalSum = totalSum;
        }
    }
}
