using CarPriceAPI.Models;
using System.Threading.Tasks;

namespace CarPriceAPI.Services
{
    public interface IHistoryService
    {
        Task AddCarHistoryDbAsync(CarHistoryModel carHistoryModel);
    }
}
