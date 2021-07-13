using BAL.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<Account>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Account>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Account>
        [HttpPost]
        public IActionResult Login([FromBody] UserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest("Invalid client request");
            }
            var result = _userService.LoginWithGetToken(userViewModel);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        // PUT api/<Account>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Account>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
