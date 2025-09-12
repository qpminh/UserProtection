using Microsoft.AspNetCore.Mvc;
using UserProtection.API.Middlewares;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureTestController : ControllerBase
    {
        // Ví dụ: API này yêu cầu Feature "Trusted Link Management"
        [HttpGet("trusted")]
        [RequireFeature("Anti-Phishing Detection")]
        public IActionResult TestTrustedLink()
        {
            return Ok(new { Message = "Bạn có quyền dùng Anti-Phishing Detection!" });
        }

        // Ví dụ: API này yêu cầu Feature "Audit Log Access"
        [HttpGet("audit")]
        [RequireFeature("Suspicious Link Scan")]
        public IActionResult TestAuditLog()
        {
            return Ok(new { Message = "Suspicious Link Scan!" });
        }

        // API này public, không yêu cầu feature
        [HttpGet("public")]
        public IActionResult TestPublic()
        {
            return Ok(new { Message = "Ai cũng gọi được API này." });
        }
    }
}
