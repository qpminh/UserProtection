using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Cores;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Controllers.Cores
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
            if (dto.TenantId == 0 || dto.TenantId == null)
            {
                dto.TenantId = null;
            }

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
