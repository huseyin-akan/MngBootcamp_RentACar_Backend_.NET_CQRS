using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdditionalServiceRental
    {
        public int AdditionalServiceId { get; set; }
        public int RentalId { get; set; }
    }
}
