using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data;
using UserManager.Model;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private DbContext _context;

        public UserController(DbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(_context.Users.Any(u=>u.Login == userDto.Login))
                return BadRequest("Login already used!");

            var user = new User
            {
                Uid = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Login = userDto.Login,
                Password = userDto.Password
            };

            _context.Users.Add(user);

            return Created("User created", user);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);
            if (user == null || user.Password != password)
                return BadRequest("Invalid login or password");

            return Ok();
        }
    }
}