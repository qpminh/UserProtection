using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UserProtection.Application.Interfaces.Cores;

namespace UserProtection.Application.Services.Cores
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;
        private readonly IAppAdminConfigService _adminCfg;

        public CurrentUserService(IHttpContextAccessor http, IAppAdminConfigService adminCfg)
        {
            _http = http;
            _adminCfg = adminCfg;
        }

        public string? UserId
        {
            get
            {
                var uid = _http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var email = UserEmail;
                if (email != null && email.Equals(_adminCfg.AdminEmail, StringComparison.OrdinalIgnoreCase))
                    return "ADMIN-FIXED-ID";

                return uid;
            }
        }

        public string? UserName =>
            _http.HttpContext?.User?.Identity?.Name;

        public string? Role =>
            _http.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        public string? UserEmail =>
            _http.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

        public int? AssociatedId
        {
            get
            {
                var value = _http.HttpContext?.User?.FindFirst("AssociatedId")?.Value;
                return int.TryParse(value, out var id) ? id : null;
            }
        }
    }
}
