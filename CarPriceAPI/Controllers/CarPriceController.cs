﻿using CarPriceAPI.JsonResults;
using CarPriceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPriceController : ControllerBase
    {
        [HttpPost]
        public JsonResult CalculatePrice(CarModel carModel)
        {
            if (carModel is null)
            {
                
            }
        }
    }
}