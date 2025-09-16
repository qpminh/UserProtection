using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentQuestionController : ControllerBase
    {
        private readonly IAssessmentQuestionService _service;

        public AssessmentQuestionController(IAssessmentQuestionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentQuestionDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("assessment/{assessmentId}")]
        public async Task<ActionResult<IEnumerable<AssessmentQuestionDto>>> GetByAssessment(int assessmentId)
        {
            var result = await _service.GetByAssessmentAsync(assessmentId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentQuestionDto>> Create([FromBody] CreateAssessmentQuestionDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssessmentQuestionDto>> Update(int id, [FromBody] UpdateAssessmentQuestionDto dto)
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
