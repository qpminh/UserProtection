using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Interfaces;

namespace UserProtection.API.Controllers.Security
{
    [ApiController]
    [Route("api/public/trustedlinks")]
    public class TrustedLinksPublicController : ControllerBase
    {
        private readonly ITrustedLinkService _svc;
        private readonly IConfiguration _config;
        public TrustedLinksPublicController(ITrustedLinkService svc, IConfiguration config) { _svc = svc; _config = config; }

        private bool ValidateApiKey()
        {
            var key = Request.Headers["X-Api-Key"].FirstOrDefault();
            var expected = _config["ExtensionApiKey"];
            return !string.IsNullOrEmpty(expected) && key == expected;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //if (!ValidateApiKey()) return Unauthorized();
            var list = await _svc.GetAll();
            return Ok(list);
        }
    }
}
