﻿
namespace HistoryAPI.Models
{
    public class CarHistoryModel
    {
        public string Company { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int Price { get; set; }

        public int UserId { get; set; }

        public string Action { get; set; }
    }
}
