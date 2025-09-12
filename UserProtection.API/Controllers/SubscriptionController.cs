using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos;
using UserProtection.Application.Services;
using UserProtection.Application.Services.Payment;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subService;

    public SubscriptionController(SubscriptionService subService)
    {
        _subService = subService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
    {
        var sub = await _subService.CreateSubscriptionAsync(request);
        return Ok(new { sub.SubscriptionId, sub.Status, sub.StartDate });
    }
}
