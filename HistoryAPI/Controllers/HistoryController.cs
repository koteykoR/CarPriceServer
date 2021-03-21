using HistoryAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HistoryAPI.BadJsonResults;
using HistoryAPI.Domain.Entities;
using HistoryAPI.Domain.Interfaces;
using HistoryAPI.Repository.Contexts;
using Microsoft.AspNetCore.Authorization;
using HistoryAPI.Repository.Implementations;

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
        [Authorize]
        public JsonResult GetHistory()
        {
            var userLogin = HttpContext.User.Identity.Name;

            var histories = _repository.FindWhere(u => u.UserLogin == userLogin);

            return new(histories);
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
                UserLogin = carHistoryModel.UserLogin,
                Action = carHistoryModel.Action
            };

            _repository.Add(car);

            await _repository.SaveAsync();

            return new("Car was added to db");
        }
    }
}
