using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Features;
using UserProtection.Application.Interfaces.Features;

namespace UserProtection.API.Controllers.Features
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService) => _featureService = featureService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _featureService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feature = await _featureService.GetByIdAsync(id);
            return feature is null ? NotFound(new { Message = "Feature not found" }) : Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeatureRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var f = await _featureService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = f.FeatureId }, f);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFeatureRequest request)
        {
            await _featureService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);
            return NoContent();
        }
    }
}