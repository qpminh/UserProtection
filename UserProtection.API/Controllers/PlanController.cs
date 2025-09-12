using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Services.Payment;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController : ControllerBase
{
    private readonly PlanService _planService;

    public PlanController(PlanService planService)
    {
        _planService = planService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlans()
    {
        var plans = await _planService.GetAllPlansAsync();
        return Ok(plans);
    }

    [HttpGet("{id}/courses")]
    public async Task<IActionResult> GetPlanCourses(int id)
    {
        var plan = await _planService.GetPlanWithCoursesAsync(id);
        if (plan == null) return NotFound();
        return Ok(plan);
    }

    [HttpGet("{id}/features")]
    public async Task<IActionResult> GetPlanFeatures(int id)
    {
        var plan = await _planService.GetPlanWithFeaturesAsync(id);
        if (plan == null) return NotFound();
        return Ok(plan);
    }
}
