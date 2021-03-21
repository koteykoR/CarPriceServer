using CarPriceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPriceAPI.Services
{
    public interface IParserServcie
    {
        public Task<IEnumerable<CarModel>> GetCars(CarModel carModel);
    }
}
