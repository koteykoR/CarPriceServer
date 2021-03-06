
namespace CarBestDealsAPI.Models
{
    public class CarBestDealFormModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public int Mileage { get; set; }

        public int EnginePower { get; set; }

        public double EngineVolume { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public bool Transmission { get; set; }
    }
}
