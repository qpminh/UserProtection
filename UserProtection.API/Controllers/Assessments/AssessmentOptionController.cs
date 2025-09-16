using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentOptionController : ControllerBase
    {
        private readonly IAssessmentOptionService _service;

        public AssessmentOptionController(IAssessmentOptionService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentOptionDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<AssessmentOptionDto>>> GetByQuestion(int questionId)
        {
            var result = await _service.GetByQuestionAsync(questionId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentOptionDto>> Create([FromBody] CreateAssessmentOptionDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssessmentOptionDto>> Update(int id, [FromBody] UpdateAssessmentOptionDto dto)
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
