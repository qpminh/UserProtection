using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Core
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
