using Microsoft.AspNetCore.Mvc;
using UserProtection.API.Middlewares;
using UserProtection.Application.Services.Security;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkScanController : ControllerBase
    {
        private readonly AntiPhishingService _scanService;

        public LinkScanController(AntiPhishingService scanService)
        {
            _scanService = scanService;
        }

        // Yêu cầu có quyền Feature "AntiPhishing" (FeatureId = 1)
        [HttpPost("check")]
        [RequireFeatureAccess(1)]
        public IActionResult CheckUrl([FromBody] UrlRequest request)
        {
            if (_scanService.IsSuspicious(request.Url, out var matched))
            {
                return Ok(new
                {
                    Url = request.Url,
                    Status = "Suspicious",
                    MatchedPattern = matched
                });
            }

            return Ok(new
            {
                Url = request.Url,
                Status = "Safe"
            });
        }
    }

    public class UrlRequest
    {
        public string Url { get; set; } = null!;
    }
}
