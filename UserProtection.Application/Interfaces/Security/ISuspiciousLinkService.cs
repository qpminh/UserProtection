using UserProtection.Application.Dtos.Security;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Security
{
    public interface ISuspiciousLinkService
    {
        Task<SuspiciousLinkDto> Report(SuspiciousLinkDto dto, string? currentUserId);
        Task<IEnumerable<SuspiciousLinkDto>> GetByUser(string userId);
        Task<IEnumerable<SuspiciousLinkDto>> GetRecent(int limit = 100);
        Task<IEnumerable<SuspiciousLink>> GetPhising();
        Task<IEnumerable<SuspiciousLink>> GetPhising(string url);
        Task Update(SuspiciousLink entity);
    }
}
