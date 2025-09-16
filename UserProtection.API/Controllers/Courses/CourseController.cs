using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Courses;
using UserProtection.Application.Interfaces.Courses;

namespace UserProtection.API.Controllers.Courses
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService) => _courseService = courseService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _courseService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return course is null ? NotFound(new { Message = "Course not found" }) : Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var c = await _courseService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = c.CourseId }, c);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _courseService.UpdateAsync(id, request);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("by-key")]
        public async Task<IActionResult> GetCoursesByKey([FromQuery] string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest(new { Message = "ApiKey is required" });

            var courses = await _courseService.GetCoursesByKeyAsync(apiKey);

            if (!courses.Any())
                return NotFound(new { Message = "No courses found for this subscription" });

            return Ok(courses);
        }
    }
}