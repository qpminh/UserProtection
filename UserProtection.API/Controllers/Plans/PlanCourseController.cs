using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Interfaces.Plans;

namespace UserProtection.API.Controllers.Plans
{
    [ApiController]
    [Route("api/plans/{planId:int}/courses")]
    public class PlanCourseController : ControllerBase
    {
        private readonly IPlanCourseService _planCourseService;
        public PlanCourseController(IPlanCourseService planCourseService) => _planCourseService = planCourseService;

        [HttpPost("{courseId:int}")]
        public async Task<IActionResult> AddCourse(int planId, int courseId)
        {
            await _planCourseService.AddCourseToPlanAsync(planId, courseId);
            return Ok(new { Message = $"Course {courseId} added to Plan {planId}" });
        }

        [HttpDelete("{courseId:int}")]
        public async Task<IActionResult> RemoveCourse(int planId, int courseId)
        {
            await _planCourseService.RemoveCourseFromPlanAsync(planId, courseId);
            return Ok(new { Message = $"Course {courseId} removed from Plan {planId}" });
        }
    }
}