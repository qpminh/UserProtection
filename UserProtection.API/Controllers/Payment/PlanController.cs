using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Services.Payment;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanController : ControllerBase
{
    private readonly PlanService _planService;
    public PlanController(PlanService planService) => _planService = planService;

    [HttpGet("active")]
    public async Task<IActionResult> GetActivePlans()
        => Ok(await _planService.GetActivePlansAsync());

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPlans()
        => Ok(await _planService.GetAllPlansAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPlanDetails(int id)
    {
        var plan = await _planService.GetPlanDetailsAsync(id);
        return plan is null ? NotFound(new { Message = "Plan not found" }) : Ok(plan);
    }

    [HttpGet("{id:int}/courses")]
    public async Task<IActionResult> GetPlanCourses(int id)
    {
        var plan = await _planService.GetPlanWithCoursesAsync(id);
        return plan is null ? NotFound(new { Message = "Plan not found" }) : Ok(plan);
    }

    [HttpGet("{id:int}/features")]
    public async Task<IActionResult> GetPlanFeatures(int id)
    {
        var plan = await _planService.GetPlanWithFeaturesAsync(id);
        return plan is null ? NotFound(new { Message = "Plan not found" }) : Ok(plan);
    }
}
