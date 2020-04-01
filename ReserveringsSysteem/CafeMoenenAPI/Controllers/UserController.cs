using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CafeMoenenAPI.Helpers;
using CafeMoenenAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeMoenenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // TODO: AFVANGEN NULL PROPERTIES


        // POST: api/user/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // TODO: Hash password

            var user = _dbContext.Users.SingleOrDefault(x =>
                x.EmailAddress.Equals(loginModel.Email) && x.PasswordHash.Equals(loginModel.Password));

            if (user == null)
            {
                return BadRequest("Cannot find this user");
            }

            return Ok(user.WithoutPassword());
        }

        // POST: api/user/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            if (registerModel == null) return BadRequest("Fields were empty");

            if (_dbContext.Users.Any(x => x.EmailAddress.Equals(registerModel.EmailAddress))) return BadRequest("User already exists");

            // TODO: Hash Password

            var user = new UserModel
            {
                UserCode = ExtensionMethods.GenerateRandomString(10),
                EmailAddress = registerModel.EmailAddress,
                PasswordHash = registerModel.Password,
                Role = Roles.User
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(user.WithoutPassword());
        }
    }
}