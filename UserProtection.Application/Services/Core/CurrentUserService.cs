using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UserProtection.Application.Interfaces;

namespace UserProtection.Application.Services.Core
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string? Role =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

        public int? AssociatedId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?.FindFirst("AssociatedId")?.Value;
                return int.TryParse(value, out var id) ? id : (int?)null;
            }
        }
    }
}
