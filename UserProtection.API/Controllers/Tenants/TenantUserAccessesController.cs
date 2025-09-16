using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Tenants;
using UserProtection.Application.Interfaces.Tenants;

namespace UserProtection.API.Controllers.Tenants
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantUserAccessesController : ControllerBase
    {
        private readonly ITenantUserAccessService _tenantUAservice;

        public TenantUserAccessesController(ITenantUserAccessService tenantUAservice)
        {
            _tenantUAservice = tenantUAservice;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var access = await _tenantUAservice.GetByIdAsync(id);
            if (access == null) return NotFound();
            return Ok(access);
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<IActionResult> GetByTenant(int tenantId)
        {
            var list = await _tenantUAservice.GetByTenantAsync(tenantId);
            return Ok(list);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var list = await _tenantUAservice.GetByUserAsync(userId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TenantUserAccessCreateDto dto)
        {
            var access = await _tenantUAservice.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = access.AccessId }, access);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TenantUserAccessUpdateDto dto)
        {
            var access = await _tenantUAservice.UpdateAsync(id, dto);
            if (access == null) return NotFound();
            return Ok(access);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tenantUAservice.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
