using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userRepo.AuthenticateUser(model.Username, model.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthenticationModel model)
        {
            bool isUserUnique = _userRepo.IsUniqueUser(model.Username);
            if (!isUserUnique)
            {
                return BadRequest(new {message = "username already exists" });
            }
            User user = _userRepo.Register(model.Username, model.Password);
            if(user == null)
            {
                return BadRequest(new { message = "An error occured while Registering" });
            }
            return Ok();
        }
    }
}