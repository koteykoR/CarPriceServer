using System;
using System.Linq;
using CarBestDealsAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarBestDealsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MiddlewareLibrary;
using System.Collections;
using System.Collections.Generic;

namespace CarBestDealsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBestDealsController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        private readonly IParserService _parserService;

        public CarBestDealsController(IHistoryService historyService, IParserService parserService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

            _parserService = parserService ?? throw new ArgumentNullException(nameof(parserService));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetCarBestDeals(CarModel carModel)
        {
            if (carModel is null) return new(new Either<CarModel[], Error>(null, Errors.CarWasNull));

            var userLogin = HttpContext.User.Identity.Name;

            var cars = await _parserService.GetCars(carModel);

            var carsBestDealDataModel = cars.Select(c => new CarBestDealDataModel
            {
                Company = c.Company,
                Model = c.Model,
                Year = c.Year,
                Price = c.Price,
                Link = c.Link
            });

            var historyModel = new CarHistoryModel
            {
                Company = carModel.Company,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = carModel.Price,
                UserLogin = userLogin,
                Action = "Get 100 best deals"
            };

            await _historyService.AddCarHistoryDbAsync(historyModel);

            return new(new Either<CarBestDealDataModel[], Error>(carsBestDealDataModel.ToArray(), null));
        }
    }
}
