using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Security
{
    public interface ISuspiciousLinkRepository
    {
        Task<SuspiciousLink> Add(SuspiciousLink entity);
        Task<IEnumerable<SuspiciousLink>> GetByUser(string userId);
        Task<IEnumerable<SuspiciousLink>> GetRecent(int limit = 100);
        Task<IEnumerable<SuspiciousLink>> GetPhising(string status);
        Task<IEnumerable<SuspiciousLink>> GetPhising(string status, string url);
        Task Update(SuspiciousLink entity);
    }
}
