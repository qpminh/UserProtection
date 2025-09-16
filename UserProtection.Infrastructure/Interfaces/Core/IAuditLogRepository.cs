using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Core
{
    public interface IAuditLogRepository
    {
        Task AddLog(AuditLog log);
    }
}
