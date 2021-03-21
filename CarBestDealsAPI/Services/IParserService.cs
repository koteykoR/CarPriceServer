using CarBestDealsAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarBestDealsAPI.Services
{
    public interface IParserService
    {
        public Task<IEnumerable<CarModel>> GetCars(CarModel carModel);
    }
}
