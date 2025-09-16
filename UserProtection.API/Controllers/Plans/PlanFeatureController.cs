using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Interfaces.Plans;

namespace UserProtection.API.Controllers.Plans
{
    [ApiController]
    [Route("api/plans/{planId:int}/features")]
    public class PlanFeatureController : ControllerBase
    {
        private readonly IPlanFeatureService _planFeatureService;
        public PlanFeatureController(IPlanFeatureService planFeatureService) => _planFeatureService = planFeatureService;

        [HttpPost("{featureId:int}")]
        public async Task<IActionResult> AddFeature(int planId, int featureId)
        {
            await _planFeatureService.AddFeatureToPlanAsync(planId, featureId);
            return Ok(new { Message = $"Feature {featureId} added to Plan {planId}" });
        }

        [HttpDelete("{featureId:int}")]
        public async Task<IActionResult> RemoveFeature(int planId, int featureId)
        {
            await _planFeatureService.RemoveFeatureFromPlanAsync(planId, featureId);
            return Ok(new { Message = $"Feature {featureId} removed from Plan {planId}" });
        }
    }
}
