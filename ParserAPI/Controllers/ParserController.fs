namespace ParserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open ParserAPI.Models
open ParserAPI.Parsers

[<ApiController>]
[<Route("api/[controller]")>]
type ParserController() = 
    inherit ControllerBase()
    
    [<HttpPost>]
    member _.Post(car: CarModel) : JsonResult = 
        let cars = AutoParser.Parse car.Company car.Model
        JsonResult(cars)
