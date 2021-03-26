using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBestDealsAPI.Models
{
    public class CarBestDealDataModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public string Link { get; set; }
    }
}
