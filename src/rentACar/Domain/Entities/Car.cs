using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Car :Entity
    {
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }

        public CarState CarState { get; set; }

        public virtual Color Color { get; set; }
        public virtual Model Model { get; set; }

        public Car()
        {

        }

        public Car(int id, int modelId, int colorId, string plate, int modelYear, CarState carState) :this()
        {
            Id = id;
            ModelId = modelId;
            ColorId = colorId;
            Plate = plate;
            ModelYear = modelYear;
            CarState = carState;
        }
    }
}
