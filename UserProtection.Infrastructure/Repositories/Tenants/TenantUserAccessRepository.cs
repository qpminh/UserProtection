using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Tenants;

namespace UserProtection.Infrastructure.Repositories.Tenants
{
    public class TenantUserAccessRepository : ITenantUserAccessRepository
    {
        private readonly UserProtectionContext _context;

        public TenantUserAccessRepository(UserProtectionContext context)
        {
            _context = context;
        }

        public async Task<TenantUserAccess?> GetByIdAsync(int accessId) =>
            await _context.TenantUserAccesses
                .Include(x => x.User)
                .Include(x => x.Tenant)
                .Include(x => x.Subscription)
                .FirstOrDefaultAsync(x => x.AccessId == accessId);

        public async Task<IEnumerable<TenantUserAccess>> GetByTenantAsync(int tenantId) =>
            await _context.TenantUserAccesses
                .Where(x => x.TenantId == tenantId)
                .Include(x => x.User)
                .ToListAsync();

        public async Task<IEnumerable<TenantUserAccess>> GetByUserAsync(string userId) =>
            await _context.TenantUserAccesses
                .Where(x => x.UserId == userId)
                .Include(x => x.Tenant)
                .ToListAsync();

        public async Task AddAsync(TenantUserAccess entity) =>
            await _context.TenantUserAccesses.AddAsync(entity);

        public void Update(TenantUserAccess entity) =>
            _context.TenantUserAccesses.Update(entity);

        public void Delete(TenantUserAccess entity) =>
            _context.TenantUserAccesses.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
