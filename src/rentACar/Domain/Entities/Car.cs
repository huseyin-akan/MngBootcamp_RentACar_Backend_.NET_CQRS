using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities
{
    public class Car :Entity
    {
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public string Plate { get; set; }
        public int ModelYear { get; set; }
        public int FindexScore { get; set; }

        public CarState CarState { get; set; }

        public virtual Color Color { get; set; }
        public virtual Model Model { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

        public Car()
        {
            Rentals = new HashSet<Rental>();
        }

        public Car(int id, int modelId, int colorId,
            string plate, int modelYear, CarState carState, int findexScore) :this()
        {
            Id = id;
            ModelId = modelId;
            ColorId = colorId;
            Plate = plate;
            ModelYear = modelYear;
            CarState = carState;
            FindexScore = findexScore;
        }
    }
}
