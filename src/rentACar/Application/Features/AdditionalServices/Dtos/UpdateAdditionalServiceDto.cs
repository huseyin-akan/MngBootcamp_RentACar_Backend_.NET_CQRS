using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Dtos
{
    public class UpdateAdditionalServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }

        public byte ServicePoint { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
