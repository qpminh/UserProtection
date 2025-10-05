using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Security;

namespace UserProtection.Infrastructure.Repositories.Security
{
    public class SuspiciousLinkRepository : ISuspiciousLinkRepository
    {
        private readonly UserProtectionContext _context;
        public SuspiciousLinkRepository(UserProtectionContext context)
        {
            _context = context;
        }

        public async Task<SuspiciousLink> Add(SuspiciousLink entity)
        {
            _context.SuspiciousLinks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<SuspiciousLink>> GetByUser(string userId)
            => await _context.SuspiciousLinks.Where(x => x.UserId == userId).OrderByDescending(x => x.DetectedAt).ToListAsync();

        public async Task<IEnumerable<SuspiciousLink>> GetRecent(int limit = 100)
            => await _context.SuspiciousLinks.OrderByDescending(x => x.DetectedAt).Take(limit).ToListAsync();

        public async Task<IEnumerable<SuspiciousLink>> GetPhising(string status)
            => await _context.SuspiciousLinks
            .Where(x => x.Status == status).ToListAsync();

        public async Task<IEnumerable<SuspiciousLink>> GetPhising(string status, string url)
            => await _context.SuspiciousLinks
            .Where(x => x.Status == status && x.Url == url).ToListAsync();

        public async Task Update(SuspiciousLink entity)
        {
            _context.SuspiciousLinks.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
