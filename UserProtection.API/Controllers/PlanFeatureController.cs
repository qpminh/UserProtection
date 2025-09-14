using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Services;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/plans/{planId:int}/features")]
public class PlanFeatureController : ControllerBase
{
    private readonly PlanFeatureService _service;
    public PlanFeatureController(PlanFeatureService service) => _service = service;

    [HttpPost("{featureId:int}")]
    public async Task<IActionResult> AddFeature(int planId, int featureId)
    {
        await _service.AddFeatureToPlanAsync(planId, featureId);
        return Ok(new { Message = $"Feature {featureId} added to Plan {planId}" });
    }

    [HttpDelete("{featureId:int}")]
    public async Task<IActionResult> RemoveFeature(int planId, int featureId)
    {
        await _service.RemoveFeatureFromPlanAsync(planId, featureId);
        return Ok(new { Message = $"Feature {featureId} removed from Plan {planId}" });
    }
}
