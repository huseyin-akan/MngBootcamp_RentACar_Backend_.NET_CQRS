using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Dtos
{
    public class CreateInvoiceDto
    {
        public int Id { get; set; }
        public long InvoiceNo { get; set; }
        public string CustomerMail { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double TotalSum { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int TotalDayCount { get; set; }
        public int RentedKilometer { get; set; }
        public string Brand { get; set; }
        public string CarModel { get; set; }
        public int ModelYear { get; set; }
        public string Plate { get; set; }
    }
}
