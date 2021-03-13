﻿namespace ParserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open ParserAPI.Models
open ParserAPI.Parsers

[<ApiController>]
[<Route("api/[controller]")>]
type ParserController() = 
    inherit ControllerBase()
    
    [<HttpGet>]
    member _.Get() = JsonResult("Hi, How Are You")

    [<HttpPost>]
    member _.Post(car: CarModel) : JsonResult = 
        let cars = AutoParser.Parse car.Company car.Model
        JsonResult(cars)
