using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _service;

        public AssessmentController(IAssessmentService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<AssessmentDto>>> GetByCourse(int courseId)
        {
            var result = await _service.GetByCourseAsync(courseId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentDto>> Create([FromBody] CreateAssessmentDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssessmentDto>> Update(int id, [FromBody] UpdateAssessmentDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
