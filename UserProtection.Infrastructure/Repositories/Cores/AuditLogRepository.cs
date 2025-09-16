using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Core;

namespace UserProtection.Infrastructure.Repositories.Cores
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly UserProtectionContext _context;

        public AuditLogRepository(UserProtectionContext db) => _context = db;

        public async Task AddLog(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
