using CarPriceAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarPriceAPI.Services
{
    public interface IParserService
    {
        public Task<IEnumerable<CarModel>> GetCars(CarModel carModel);
    }
}
