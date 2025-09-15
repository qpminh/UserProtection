using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos;
using UserProtection.Application.Dtos.Subscription;
using UserProtection.Application.Services.Subscription;

namespace UserProtection.API.Controllers.Subscription;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subService;
    private readonly SubscriptionKeyService _keyService;

    public SubscriptionController(SubscriptionService subService, SubscriptionKeyService keyService)
    {
        _subService = subService;
        _keyService = keyService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var sub = await _subService.CreateSubscriptionAsync(request);
        return CreatedAtAction(nameof(CreateSubscription), new { id = sub.SubscriptionId }, sub);
    }

    [HttpGet("{id}/key")]
    public async Task<IActionResult> GetKeys(int id)
    {
        var keys = await _keyService.GetKeysAsync(id);
        if (!keys.Any())
            return NotFound(new { Message = "No keys found for this subscription" });

        return Ok(keys.Select(k => new SubscriptionKeyDto
        {
            KeyValue = k.KeyValue,
            CreatedAt = k.CreatedAt,
            IsActive = k.IsActive
        }));
    }

    [HttpPost("{id}/key/regenerate")]
    public async Task<IActionResult> RegenerateKey(int id)
    {
        var newKey = await _keyService.GenerateKeyAsync(id, deactivateOld: true);
        return Ok(new { SubscriptionId = id, ApiKey = newKey });
    }
}