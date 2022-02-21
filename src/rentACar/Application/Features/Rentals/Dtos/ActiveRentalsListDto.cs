using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Dtos
{
    public class ActiveRentalsListDto
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public string RentCity { get; set; }
        public int ReturnCityId { get; set; }
        public string ReturnCity { get; set; }
        public int RentedKilometer { get; set; }
        public int CarId { get; set; }

        public string ModelName { get; set; }
        public int ModelYear { get; set; }
        public int CustomerId { get; set; }
    }
}
