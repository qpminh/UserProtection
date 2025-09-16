using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Assessments;
using UserProtection.Application.Interfaces.Assessments;

namespace UserProtection.API.Controllers.Assessments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentAttemptController : ControllerBase
    {
        private readonly IAssessmentAttemptService _service;

        public AssessmentAttemptController(IAssessmentAttemptService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AssessmentAttemptDto>> StartAttempt([FromBody] CreateAssessmentAttemptDto dto)
        {
            var result = await _service.StartAttemptAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssessmentAttemptDto>> GetAttempt(int id)
        {
            var result = await _service.GetAttemptAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
