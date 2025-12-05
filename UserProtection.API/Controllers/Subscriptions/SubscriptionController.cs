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
            var userId = !string.IsNullOrWhiteSpace(request.UserId)
                ? request.UserId
                : _currentUserService.UserId;

            if (userId == null)
                return Unauthorized();

            var sub = await _subService.CreatePendingSubscriptionAsync(request.PlanId, userId);
            return Ok(sub);
        }

        [HttpPatch("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateSubscriptionStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Status))
                return BadRequest(new { Message = "Status is required." });

            var result = await _subService.UpdateStatusAsync(id, request.Status);
            if (result == null)
                return NotFound(new { Message = "Subscription not found." });

            return Ok(result);
        }

        [HttpGet("pending")]
        [Authorize]
        public async Task<IActionResult> GetPending() =>
            Ok(await _subService.GetPendingAsync());

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() =>
            Ok(await _subService.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var sub = await _subService.GetByIdAsync(id);
            if (sub == null)
                return NotFound(new { Message = "Subscription not found." });

            return Ok(sub);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMySubscription()
        {
            var result = await _subService.GetCurrentUserSubscriptionAsync();
            if (result == null)
                return NotFound(new { Message = "You have no subscription." });

            return Ok(result);
        }

        [HttpPatch("{id}/dates")]
        [Authorize]
        public async Task<IActionResult> UpdateSubscriptionDates(int id, [FromBody] UpdateSubscriptionDatesRequest request)
        {
            if (request.StartDate == null && request.EndDate == null)
                return BadRequest(new { Message = "At least one field must be provided (StartDate or EndDate)." });

            var result = await _subService.UpdateDatesAsync(id, request.StartDate, request.EndDate);

            if (result == null)
                return NotFound(new { Message = "Subscription not found." });

            return Ok(result);
        }
    }
}
