using HistoryAPI.BadJsonResults;
using HistoryAPI.Domain.Entities;
using HistoryAPI.Domain.Interfaces;
using HistoryAPI.Models;
using HistoryAPI.Repository.Contexts;
using HistoryAPI.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IRepository<CarHistory> _repository;

        public HistoryController(HistoryContext historyContext)
        {
            _repository = new DBRepository<CarHistory>(historyContext);
        }

        [HttpGet]
        public JsonResult GetCarsHistory()
        {
            return new(_repository.FindAll());
        }

        [HttpPost]
        public async Task<JsonResult> AddCarDb(CarHistoryModel carHistoryModel)
        {
            if (carHistoryModel is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.CarWasNull);

            var car = new CarHistory()
            {
                Company = carHistoryModel.Company,
                Model = carHistoryModel.Model,
                Year = carHistoryModel.Year,
                Price = carHistoryModel.Price,
                UserId = carHistoryModel.UserId
            };

            _repository.Add(car);

            await _repository.SaveAsync();

            return new("Ok");
        }
    }
}
