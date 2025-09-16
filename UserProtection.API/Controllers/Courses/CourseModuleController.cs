using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Course;
using UserProtection.Application.Interfaces.Courses;

namespace UserProtection.API.Controllers.Courses
{
    [ApiController]
    [Route("api/courses/{courseId:int}/modules")]
    public class CourseModuleController : ControllerBase
    {
        private readonly ICourseModuleService _courseModuleService;

        public CourseModuleController(ICourseModuleService courseModuleService) => _courseModuleService = courseModuleService;

        [HttpGet]
        public async Task<IActionResult> GetByCourse(int courseId) =>
            Ok(await _courseModuleService.GetByCourseAsync(courseId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var module = await _courseModuleService.GetByIdAsync(id);
            return module is null ? NotFound(new { Message = "Module not found" }) : Ok(module);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int courseId, [FromBody] CreateCourseModuleRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var module = await _courseModuleService.CreateAsync(courseId, request);
            return CreatedAtAction(nameof(GetById), new { courseId, id = module.ModuleId }, module);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseModuleRequest request)
        {
            await _courseModuleService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseModuleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
