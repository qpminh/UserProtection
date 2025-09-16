using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Interfaces.Security;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuspiciousLinksController : ControllerBase
    {
        private readonly ISuspiciousLinkService _svc;
        public SuspiciousLinksController(ISuspiciousLinkService svc) { _svc = svc; }

        // Extension reports
        [HttpPost("report")]
        public async Task<IActionResult> Report([FromBody] SuspiciousLinkDto dto)
        {
            // optionally validate API key similar to above
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // may be null for extension
            var created = await _svc.Report(dto, userId);
            return CreatedAtAction(nameof(GetForUser), new { userId = created.UserId ?? "unknown" }, created);
        }

        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetForUser(string userId)
        {
            var list = await _svc.GetByUser(userId);
            return Ok(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("recent")]
        public async Task<IActionResult> Recent([FromQuery] int limit = 100)
        {
            var list = await _svc.GetRecent(limit);
            return Ok(list);
        }
    }

}
