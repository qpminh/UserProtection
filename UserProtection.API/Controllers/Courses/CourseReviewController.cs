using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Interfaces.Courses;

namespace UserProtection.API.Controllers.Courses
{
    [ApiController]
    [Route("api/courses/{courseId:int}/reviews")]
    public class CourseReviewController : ControllerBase
    {
        private readonly ICourseReviewService _courseReviewService;

        public CourseReviewController(ICourseReviewService courseModuleService) => _courseReviewService = courseModuleService;

        [HttpGet]
        public async Task<IActionResult> GetByCourse(int courseId) =>
            Ok(await _courseReviewService.GetByCourseAsync(courseId));

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(int courseId, [FromBody] CreateCourseReviewRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var review = await _courseReviewService.CreateOrUpdateAsync(courseId, request);
                return Ok(review);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseReviewService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
