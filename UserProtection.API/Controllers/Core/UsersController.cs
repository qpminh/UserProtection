using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Interfaces;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Controllers.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

    }
}
