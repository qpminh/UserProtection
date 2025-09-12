using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Core;
using UserProtection.Application.Interfaces;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Controllers.Core
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                TenantId = dto.TenantId,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            var result = await _userService.Register(user, dto.Password);
            return result.Succeeded ? Ok("Registered successfully") : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _userService.Login(dto.Email, dto.Password);
            return Ok(new { Token = token });
        }
    }
}
