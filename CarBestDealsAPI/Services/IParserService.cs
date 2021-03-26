using CarBestDealsAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarBestDealsAPI.Domains;

namespace CarBestDealsAPI.Services
{
    public interface IParserService
    {
        public Task<IEnumerable<Car>> GetCars(Car car);
    }
}
