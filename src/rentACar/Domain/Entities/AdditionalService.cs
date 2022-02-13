using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdditionalService :Entity
    {
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }

        public byte ServicePoint { get; set; }
        public ServiceType ServiceType { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

        public AdditionalService()
        {
            Rentals = new HashSet<Rental>();   
        }

    }
}
