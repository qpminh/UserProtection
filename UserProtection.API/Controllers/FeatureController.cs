using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos;
using UserProtection.Application.Services;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeatureController : ControllerBase
{
    private readonly FeatureService _service;

    public FeatureController(FeatureService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var feature = await _service.GetByIdAsync(id);
        return feature == null ? NotFound() : Ok(feature);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFeatureRequest request)
    {
        var f = await _service.CreateAsync(request);
        return Ok(f);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateFeatureRequest request)
    {
        await _service.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}