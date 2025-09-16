using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Interfaces.Security;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class TrustedLinksController : ControllerBase
    {
        private readonly ITrustedLinkService _svc;
        public TrustedLinksController(ITrustedLinkService svc) { _svc = svc; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _svc.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrustedLinkDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var created = await _svc.Create(dto, userId);
            return CreatedAtAction(nameof(Get), new { id = created.LinkId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrustedLinkDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var updated = await _svc.Update(id, dto, userId);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ok = await _svc.Delete(id, userId);
            return ok ? NoContent() : NotFound();
        }
    }
}
