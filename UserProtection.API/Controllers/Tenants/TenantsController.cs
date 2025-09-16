using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Tenants;
using UserProtection.Application.Interfaces.Tenants;

namespace UserProtection.API.Controllers.Tenants
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _tenantService.GetAllAsync();
            return Ok(tenants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tenant = await _tenantService.GetByIdAsync(id);
            if (tenant == null) return NotFound();
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TenantCreateDto dto)
        {
            var tenant = await _tenantService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = tenant.TenantId }, tenant);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TenantUpdateDto dto)
        {
            var tenant = await _tenantService.UpdateAsync(id, dto);
            if (tenant == null) return NotFound();
            return Ok(tenant);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tenantService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
