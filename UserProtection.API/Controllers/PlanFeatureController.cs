using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Services;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanFeatureController : ControllerBase
{
    private readonly PlanFeatureService _service;

    public PlanFeatureController(PlanFeatureService service) => _service = service;

    [HttpPost("{planId}/add/{featureId}")]
    public async Task<IActionResult> AddFeature(int planId, int featureId)
    {
        await _service.AddFeatureToPlanAsync(planId, featureId);
        return Ok(new { Message = "Feature added to plan" });
    }

    [HttpDelete("{planId}/remove/{featureId}")]
    public async Task<IActionResult> RemoveFeature(int planId, int featureId)
    {
        await _service.RemoveFeatureFromPlanAsync(planId, featureId);
        return Ok(new { Message = "Feature removed from plan" });
    }
}