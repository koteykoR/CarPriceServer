using System;
using System.Linq;
using CarPriceAPI.Models;
using CarPriceAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiddlewareLibrary;
using AutoMapper;
using CarPriceAPI.Domains;

namespace CarPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPriceController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly IParserService _parserService;

        private readonly IMapper _mapper;

        public CarPriceController(IHistoryService historyService, IParserService parserServcie, IMapper mapper)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));

            _parserService = parserServcie ?? throw new ArgumentNullException(nameof(parserServcie));

            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CalculatePrice(CarModel carModel)
        {
            if (carModel is null) return new(new Either<int, Error>(0, Errors.CarWasNull));

            var userLogin = HttpContext.User.Identity.Name;

            var car = _mapper.Map<Car>(carModel);

            var cars = await _parserService.GetCars(car);          

            var price = cars.Aggregate(-1000, (x, y) => x + y.Price);

            var historyModel = _mapper.Map<CarHistoryModel>(car);

            historyModel.UserLogin = userLogin;
            historyModel.Price = price;

            await _historyService.AddCarHistoryDbAsync(historyModel);

            return new(new Either<int, Error>(price, null));
        }
    }
}
