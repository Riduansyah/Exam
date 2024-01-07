using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost("add")]
        public IActionResult AddUser([FromQuery] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.Add(model);

            return Ok(model);
        }
    }
}
