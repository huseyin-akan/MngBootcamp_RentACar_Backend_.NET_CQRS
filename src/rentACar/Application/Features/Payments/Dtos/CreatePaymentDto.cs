using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Dtos
{
    public class CreatePaymentDto
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalSum { get; set; }
        public int RentalId { get; set; }
    }
}
