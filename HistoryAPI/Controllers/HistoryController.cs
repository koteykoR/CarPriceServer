using HistoryAPI.BadJsonResults;
using HistoryAPI.Domain.Entities;
using HistoryAPI.Domain.Interfaces;
using HistoryAPI.Models;
using HistoryAPI.Repository.Contexts;
using HistoryAPI.Repository.Implementations;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<JsonResult> GetCarsHistory()
        {
            var userLogin = HttpContext.User.Identity.Name;

            var history = new CarHistory
            {
                Company = "Lifan",
                Model = "x50",
                Price = 12213123,
                UserLogin = userLogin
            };

            _repository.Add(history);

            await _repository.SaveAsync();

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
                UserLogin = carHistoryModel.UserLogin,
            };

            _repository.Add(car);

            await _repository.SaveAsync();

            return new("Ok");
        }
    }
}
