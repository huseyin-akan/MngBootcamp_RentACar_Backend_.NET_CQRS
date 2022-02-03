using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Dtos
{
    public class ModelListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DailyPrice { get; set; }
        public int TransmissionId { get; set; }
        public int FuelId { get; set; }
        public int BrandId { get; set; }
        public string ImageUrl { get; set; }
        public Brand Brand { get; set; }
        public Fuel Fuel { get; set; }
        public Transmission Transmission { get; set; }
    }
}
