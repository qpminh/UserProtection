using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentAnswerController : ControllerBase
    {
        private readonly IAssessmentAnswerService _service;

        public AssessmentAnswerController(IAssessmentAnswerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentAnswerDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("attempt/{attemptId}")]
        public async Task<ActionResult<IEnumerable<AssessmentAnswerDto>>> GetByAttempt(int attemptId)
        {
            var result = await _service.GetByAttemptAsync(attemptId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentAnswerDto>> Create([FromBody] AssessmentAnswerDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssessmentAnswerDto>> Update(int id, [FromBody] AssessmentAnswerDto dto)
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
