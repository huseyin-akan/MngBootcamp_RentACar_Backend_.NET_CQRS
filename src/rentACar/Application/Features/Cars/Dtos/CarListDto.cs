using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Dtos
{
    public class CarListDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string City { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public double DailyPrice { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public CarState CarState { get; set; }
    }
}
