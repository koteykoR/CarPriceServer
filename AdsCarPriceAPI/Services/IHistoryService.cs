using AdsCarPriceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdsCarPriceAPI.Services
{
    public interface IHistoryService
    {
        Task AddCarHistoryDbAsync(CarHistoryModel carHistoryModel);
    }
}
