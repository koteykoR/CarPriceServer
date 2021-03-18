using AdsCarPriceAPI.BadJsonResults;
using AdsCarPriceAPI.Models;
using AdsCarPriceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdsCarPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsCarPriceController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        private readonly IParserService _parserService;

        public AdsCarPriceController(IHistoryService historyService, IParserService parserService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

            _parserService = parserService ?? throw new ArgumentNullException(nameof(parserService));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CalculatePrice(CarModel carModel)
        {
            if (carModel is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.CarWasNull);

            var userLogin = HttpContext.User.Identity.Name;

            var cars = await _parserService.GetCars(carModel);

            var price = cars.Aggregate(-1000, (x, y) => x + y.Price); // тут питон

            var historyModel = new CarHistoryModel
            {
                Company = carModel.Company,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = price,
                UserLogin = userLogin,
                Action = "Calcualte car price base on other casrs"
            };

            await _historyService.AddCarHistoryDbAsync(historyModel);

            return new(historyModel);
        }
    }
}
