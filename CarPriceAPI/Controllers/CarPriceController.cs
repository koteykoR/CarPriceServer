using System;
using System.Threading.Tasks;
using CarPriceAPI.BadJsonResults;
using CarPriceAPI.Models;
using CarPriceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPriceController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public CarPriceController(IHistoryService historyService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        }

        [HttpGet]
        public async Task<JsonResult> GetSomething()
        {
            var carHistory = new CarHistoryModel
            {
                Company = "max",
                Model = "max",
                Year = DateTime.Now,
                Price = 1000,
                UserId = 1
            };

            await _historyService.AddCarHistoryDbAsync(carHistory);

            return new("hello");
        }

        [HttpPost]
        public JsonResult CalculatePrice(CarModel carModel)
        {
            if (carModel is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.CarWasNull);

            decimal price = carModel.Mileage + carModel.EnginePower;

            return new(price);
        }
    }
}
