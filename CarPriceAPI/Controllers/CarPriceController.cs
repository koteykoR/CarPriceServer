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

        private readonly IParserService _parserService;

        public CarPriceController(IHistoryService historyService, IParserService parserService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

            _parserService = parserService ?? throw new ArgumentNullException(nameof(parserService));
        }

        [HttpGet]
        public async Task<JsonResult> GetSomethingTest()
        {
            var cars = await _parserService.GetCars(new() { Company = "lifan", Model = "x50" });

            return new(cars);
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
