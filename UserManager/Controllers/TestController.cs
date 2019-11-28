using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Model;
using UserManager.Tools;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("Test")]
        public IActionResult GetAuthorizedValue()
        {
            return Ok(88);
        }

        [HttpGet]
        [Route("Token")]
        public IActionResult Token()
        {
            return Ok(TokenGenerator.Generate());
        }

        [HttpGet]
        [Route("TokenValidation")]
        public IActionResult TokenValidation([FromBody] Token token)
        {
            return Ok(TokenGenerator.Validate(token.Value));
        }
    }
}