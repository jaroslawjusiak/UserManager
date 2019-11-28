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


        [HttpGet]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(_context.Users.Any(u=>u.FirstName == userDto.FirstName && u.LastName == userDto.LastName))
                return BadRequest();

            var user = new User
            {
                Uid = Guid.NewGuid(),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email
            };

            _context.Users.Add(user);

            return Created("User created", user);
        }
    }
}