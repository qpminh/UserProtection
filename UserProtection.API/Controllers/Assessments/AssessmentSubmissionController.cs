using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentSubmissionController : ControllerBase
    {
        private readonly IAssessmentSubmissionService _service;

        public AssessmentSubmissionController(IAssessmentSubmissionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentSubmissionDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("assessment/{assessmentId}")]
        public async Task<ActionResult<IEnumerable<AssessmentSubmissionDto>>> GetByAssessment(int assessmentId)
        {
            var result = await _service.GetByAssessmentAsync(assessmentId);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AssessmentSubmissionDto>>> GetByUser(string userId)
        {
            var result = await _service.GetByUserAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentSubmissionDto>> Create([FromBody] CreateAssessmentSubmissionDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssessmentSubmissionDto>> Update(int id, [FromBody] UpdateAssessmentSubmissionDto dto)
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
