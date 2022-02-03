using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rental :Entity
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public Rental()
        {

        }

        public Rental(int id, DateTime rentDate, DateTime returnDate,
            DateTime returnedDate, int rentedKilometer, int returnedKilometer, int carId) :this()
        {
            Id = id;
            RentDate = rentDate;
            ReturnDate = returnDate;
            ReturnedDate = returnedDate;
            RentedKilometer = rentedKilometer;
            ReturnedKilometer = returnedKilometer;
            this.CarId = carId;
        }
    }
}
