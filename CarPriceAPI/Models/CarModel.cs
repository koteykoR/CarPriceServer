using System;

namespace CarPriceAPI.Models
{
    public sealed class CarModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public int Mileage { get; set; }

        public int EnginePower { get; set; }

        public double EngineVolume { get; set; }

        public DateTime Year { get; set; }

        public bool Transmission { get; set; }

        public decimal Price { get; set; }
    }
}
