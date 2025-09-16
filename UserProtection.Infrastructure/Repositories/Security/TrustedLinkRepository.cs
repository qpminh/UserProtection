using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Security;

namespace UserProtection.Infrastructure.Repositories.Security
{
    public class TrustedLinkRepository : ITrustedLinkRepository
    {
        private readonly UserProtectionContext _context;
        public TrustedLinkRepository(UserProtectionContext context)
        {
            _context = context;
        }

        public async Task<TrustedLink> Add(TrustedLink entity)
        {
            _context.TrustedLinks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var e = await _context.TrustedLinks.FindAsync(id);
            if (e != null) { _context.TrustedLinks.Remove(e); await _context.SaveChangesAsync(); }
        }

        public async Task<IEnumerable<TrustedLink>> GetAll(int? tenantId = null)
        {
            var q = _context.TrustedLinks.AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.TenantId == tenantId.Value);
            return await q.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public Task<IEnumerable<TrustedLink>> GetByDomain(string domain)
        {
            return Task.FromResult(_context.TrustedLinks.Where(x => x.Domain == domain).AsEnumerable());
        }

        public async Task<TrustedLink?> GetById(int id)
            => await _context.TrustedLinks.FindAsync(id);

        public async Task Update(TrustedLink entity)
        {
            _context.TrustedLinks.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
