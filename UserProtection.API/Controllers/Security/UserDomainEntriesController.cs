using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Application.Interfaces.Security;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDomainEntriesController : ControllerBase
    {
        private readonly IUserDomainEntriesService _svc;
        private readonly ICurrentUserService _currentUser;

        public UserDomainEntriesController(IUserDomainEntriesService svc, ICurrentUserService currentUser)
        {
            _svc = svc;
            _currentUser = currentUser;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _svc.GetAll();
            return Ok(res);
        }

        [HttpPost("get")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetByUserId()
        {
            if (string.IsNullOrEmpty(_currentUser.UserId))
            {
                return Unauthorized("User ID is missing.");
            }

            var res = await _svc.GetByUserId(_currentUser.UserId);
            return Ok(res);
        }

        [HttpGet("{status}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetByUserId(string status)
        {
            if (string.IsNullOrEmpty(_currentUser.UserId))
            {
                return Unauthorized("User ID is missing.");
            }

            var res = await _svc.GetByUserId(_currentUser.UserId, status);
            return Ok(res);
        }
    }
}
