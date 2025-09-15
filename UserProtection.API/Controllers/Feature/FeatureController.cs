using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Feature;
using UserProtection.Application.Services.Feature;

namespace UserProtection.API.Controllers.Feature;

[ApiController]
[Route("api/[controller]")]
public class FeatureController : ControllerBase
{
    private readonly FeatureService _service;
    public FeatureController(FeatureService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var feature = await _service.GetByIdAsync(id);
        return feature is null ? NotFound(new { Message = "Feature not found" }) : Ok(feature);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFeatureRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var f = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = f.FeatureId }, f);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateFeatureRequest request)
    {
        await _service.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
