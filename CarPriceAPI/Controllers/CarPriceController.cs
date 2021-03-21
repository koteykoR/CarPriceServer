using System;
using System.Linq;
using System.Threading.Tasks;
using CarPriceAPI.BadJsonResults;
using CarPriceAPI.Models;
using CarPriceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPriceController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IParserServcie _parserService;

        public CarPriceController(IHistoryService historyService, IParserServcie parserServcie)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

            _parserService = parserServcie ?? throw new ArgumentNullException(nameof(parserServcie));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CalculatePrice(CarModel carModel)
        {
            if (carModel is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.CarWasNull);

            var userLogin = HttpContext.User.Identity.Name;

            var cars = await _parserService.GetCars(carModel);

            var price = cars.Aggregate(-1000, (x, y) => x + y.Price);

            var historyModel = new CarHistoryModel
            {
                Company = carModel.Company,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = price,
                UserLogin = userLogin,
                Action = "Calcualte car price on base data its car"
            };

            await _historyService.AddCarHistoryDbAsync(historyModel);

            return new(price);
        }
    }
}
