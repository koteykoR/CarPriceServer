using IdentityAPI.BadJsonResults;
using IdentityAPI.Domain.Entities;
using IdentityAPI.Domain.Interfaces;
using IdentityAPI.Models;
using IdentityAPI.Repository.Contexts;
using IdentityAPI.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        private readonly IOptions<Audience> _options;

        public IdentityController(UserContext context, IOptions<Audience> options)
        {
            _repository = new DBRepository<User>(context);

            _options = options;
        }

        [HttpPost]
        public JsonResult Token(UserModel user)
        {
            var identity = GetIdentity(user);

            if (identity is null) return BadJsonResultBuilder.BuildBadJsonResult(Errors.UserNotFound);

            var signInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Secret));

            var jwt = new JwtSecurityToken
                         (issuer: _options.Value.Iss,
                          audience: _options.Value.Aud,
                          notBefore: DateTime.UtcNow,
                          claims: identity.Claims,
                          expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                          signingCredentials: new(signInKey, SecurityAlgorithms.HmacSha256)
                         );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new(encodedJwt);
        }

        private ClaimsIdentity GetIdentity(UserModel userModel)
        {
            var user = _repository.FindWhere(c => c.Login == userModel.Login && c.Password == userModel.Password)
                                  .FirstOrDefault();


            if (user is null) return null;

            var claims = new List<Claim>
                {
                    new (ClaimsIdentity.DefaultNameClaimType, user.Login)
                };

            ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }

    public class Audience
    {
        public string Secret { get; set; }

        public string Iss { get; set; }

        public string Aud { get; set; }
    }
}
