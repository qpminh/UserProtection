using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Application.Interfaces.Cores;

namespace UserProtection.API.Controllers.Subscriptions
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subService;
        private readonly ICurrentUserService _currentUserService;

        public SubscriptionController(ISubscriptionService subService, ICurrentUserService currentUserService)
        {
            _subService = subService;
            _currentUserService = currentUserService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
        {
            var targetUserId = !string.IsNullOrWhiteSpace(request.UserId)
                ? request.UserId
                : _currentUserService.UserId;

            if (targetUserId == null)
                return Unauthorized();

            var sub = await _subService.CreatePendingSubscriptionAsync(request.PlanId, targetUserId);
            return Ok(sub);
        }

        [HttpGet("pending")]
        [Authorize]
        public async Task<IActionResult> GetPending()
        {
            var subs = await _subService.GetPendingAsync();
            return Ok(subs);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var subs = await _subService.GetAllAsync();
            return Ok(subs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var sub = await _subService.GetByIdAsync(id);
            if (sub == null)
                return NotFound(new { Message = "Subscription not found." });

            return Ok(sub);
        }

        [HttpPatch("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateSubscriptionStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Status))
                return BadRequest(new { Message = "Status is required." });

            var sub = await _subService.UpdateStatusAsync(id, request.Status);

            if (sub == null)
                return NotFound(new { Message = "Subscription not found." });

            return Ok(sub);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMySubscription()
        {
            var info = await _subService.GetCurrentUserSubscriptionAsync();
            if (info == null)
                return NotFound(new { Message = "You have no active subscription." });

            return Ok(info);
        }
    }
}
