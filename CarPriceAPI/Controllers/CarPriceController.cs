﻿using System;
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

        public CarPriceController(IHistoryService historyService)
        {
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CalculatePrice(CarModel carModel)
        {
            if (carModel is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.CarWasNull);

            var userLogin = HttpContext.User.Identity.Name;

            var historyModel = new CarHistoryModel
            {
                Company = carModel.Company,
                Model = carModel.Model,
                Year = carModel.Year,
                Price = carModel.Mileage + carModel.EnginePower,
                UserLogin = userLogin,
                Action = "Calcualte car price on base data its car"
            };

            await _historyService.AddCarHistoryDbAsync(historyModel);

            return new(historyModel);
        }
    }
}
