﻿using Core.Persistence.Repositories;
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

        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int? ReturnedCityId { get; set; }


        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }

        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public Car Car { get; set; }
        public Customer Customer{ get; set; }
        public City RentCity { get; set; }
        public City ReturnCity { get; set; }
        public City ReturnedCity { get; set; }

        public virtual ICollection<AdditionalService> AdditionalServices { get; set; }

        public Rental()
        {
            AdditionalServices = new HashSet<AdditionalService>();  
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
