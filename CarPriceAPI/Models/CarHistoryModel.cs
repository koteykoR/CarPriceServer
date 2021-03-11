using System;

namespace CarPriceAPI.Models
{
    public class CarHistoryModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}
