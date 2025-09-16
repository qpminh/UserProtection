using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Interfaces.Subscriptions;

namespace UserProtection.API.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subService;
        private readonly ISubscriptionKeyService _keyService;

        public SubscriptionController(ISubscriptionService subService, ISubscriptionKeyService keyService)
        {
            _subService = subService;
            _keyService = keyService;
        }

        // -----------------------
        // CRUD Subscription
        // -----------------------

        [HttpPost("tenant/{tenantId}")]
        public async Task<IActionResult> CreateSubscription(
            int tenantId,
            [FromBody] CreateSubscriptionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var sub = await _subService.CreateSubscriptionAsync(request, tenantId);
            return CreatedAtAction(nameof(GetById), new { id = sub.SubscriptionId }, sub);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sub = await _subService.GetByIdAsync(id);
            if (sub == null) return NotFound();
            return Ok(sub);
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<IActionResult> GetByTenant(int tenantId)
        {
            var list = await _subService.GetByTenantAsync(tenantId);
            return Ok(list);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubscriptionRequest dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var sub = await _subService.UpdateAsync(id, dto);
            if (sub == null) return NotFound();
            return Ok(sub);
        }

        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _subService.CancelAsync(id);
            if (!result) return NotFound();
            return Ok(new { Message = "Subscription cancelled" });
        }

        // -----------------------
        // Subscription Keys
        // -----------------------

        [HttpGet("{id:int}/key")]
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

        [HttpPost("{id:int}/key/regenerate")]
        public async Task<IActionResult> RegenerateKey(int id)
        {
            var newKey = await _keyService.GenerateKeyAsync(id, deactivateOld: true);
            return Ok(new { SubscriptionId = id, ApiKey = newKey });
        }
    }
}
