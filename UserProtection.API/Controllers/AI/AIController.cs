using Microsoft.AspNetCore.Mvc;
using System.Text;
using UserProtection.Application.Dtos.AI;
using UserProtection.Application.Interfaces.Gemini;

namespace UserProtection.API.Controllers.AI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AIRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("Nội dung phân tích không được để trống.");

            var query = request.Prompt.Trim();

            var result = await _aiService.AskGeminiAsync(query);

            return Ok(result);
        }
    }
}
