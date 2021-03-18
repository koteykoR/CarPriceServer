using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestDealsCarPriceAPI.Models
{
    public class CarModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public int Mileage { get; set; }

        public int EnginePower { get; set; }

        public double EngineVolume { get; set; }

        public int Year { get; set; }

        public bool Transmission { get; set; }

        public int Price { get; set; }

        public string Link { get; set; }
    }
}
