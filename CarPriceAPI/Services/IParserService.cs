using System.Threading.Tasks;
using System.Collections.Generic;
using CarPriceAPI.Domains;

namespace CarPriceAPI.Services
{
    public interface IParserService
    {
        public Task<IEnumerable<Car>> GetCars(Car car);
    }
}
