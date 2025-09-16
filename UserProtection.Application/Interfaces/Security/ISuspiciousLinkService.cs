using UserProtection.Application.Dtos.Security;

namespace UserProtection.Application.Interfaces.Security
{
    public interface ISuspiciousLinkService
    {
        Task<SuspiciousLinkDto> Report(SuspiciousLinkDto dto, string? currentUserId);
        Task<IEnumerable<SuspiciousLinkDto>> GetByUser(string userId);
        Task<IEnumerable<SuspiciousLinkDto>> GetRecent(int limit = 100);
    }
}
