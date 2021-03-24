using IdentityAPI.Domain.Entities;
using IdentityAPI.Domain.Interfaces;
using IdentityAPI.Models;
using IdentityAPI.Repository.Contexts;
using IdentityAPI.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;
using MiddlewareLibrary;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public SignUpController(UserContext context)
        {
            _repository = new DBRepository<User>(context);
        }

        [HttpPost]
        public async Task<JsonResult> SignUp(UserModel userModel)
        {
            if (userModel is null) return new(new Either<bool, Error>(false, Errors.UserWasNull));

            var users = _repository.FindAll();

            var existUser = users.Where(u => u.Login == userModel.Login).FirstOrDefault();

            if (existUser is not null) return new(new Either<bool, Error>(false, Errors.UserAlreadyInDb));

            var user = new User
            {
                Login = userModel.Login,
                Password = userModel.Password
            };

            _repository.Add(user);

            await _repository.SaveAsync();

            return new(new Either<bool, Error>(true, null));
        }
    }
}
