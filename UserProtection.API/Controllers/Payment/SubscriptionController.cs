using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos;
using UserProtection.Application.Services;
using UserProtection.Application.Services.Payment;

namespace UserProtection.API.Controllers.Payment;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subService;
    public SubscriptionController(SubscriptionService subService) => _subService = subService;

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var sub = await _subService.CreateSubscriptionAsync(request);
        return CreatedAtAction(nameof(CreateSubscription), new { id = sub.SubscriptionId }, sub);
    }
}