using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Services;

namespace UserProtection.API.Controllers;

[ApiController]
[Route("api/plans/{planId:int}/courses")]
public class PlanCourseController : ControllerBase
{
    private readonly PlanCourseService _service;
    public PlanCourseController(PlanCourseService service) => _service = service;

    [HttpPost("{courseId:int}")]
    public async Task<IActionResult> AddCourse(int planId, int courseId)
    {
        await _service.AddCourseToPlanAsync(planId, courseId);
        return Ok(new { Message = $"Course {courseId} added to Plan {planId}" });
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> RemoveCourse(int planId, int courseId)
    {
        await _service.RemoveCourseFromPlanAsync(planId, courseId);
        return Ok(new { Message = $"Course {courseId} removed from Plan {planId}" });
    }
}
