using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data;
using UserManager.Model;
using UserManager.Tools;

namespace UserManager.Controllers.V2
{
    [ApiController]
    [Route("/api/[controller]")]
    [ApiVersion("2")]
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

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Uid == id);
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Users);
        }
    }
}