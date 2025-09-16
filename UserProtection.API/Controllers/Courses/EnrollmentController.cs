using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Application.Services.Courses;

namespace UserProtection.API.Controllers.Courses
{
    [ApiController]
    [Route("api/courses/{courseId:int}/enrollments")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(EnrollmentService enrollmentService) => _enrollmentService = enrollmentService;

        [HttpGet]
        public async Task<IActionResult> GetByCourse(int courseId) =>
            Ok(await _enrollmentService.GetByCourseAsync(courseId));

        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId, [FromBody] CreateEnrollmentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var enrollment = await _enrollmentService.CreateAsync(courseId, request);
            return CreatedAtAction(nameof(GetByCourse), new { courseId }, enrollment);
        }

        [HttpPost("{id:int}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _enrollmentService.CancelAsync(id);
            return result ? Ok(new { Message = "Enrollment cancelled" }) : NotFound();
        }
    }
}
