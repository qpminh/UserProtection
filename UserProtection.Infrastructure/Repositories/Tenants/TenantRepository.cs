using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Tenants;

namespace UserProtection.Infrastructure.Repositories.Tenants
{
    public class TenantRepository : ITenantRepository
    {
        private readonly UserProtectionContext _context;

        public TenantRepository(UserProtectionContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Tenant?> GetByIdAsync(int tenantId) =>
            await _context.Tenants.FirstOrDefaultAsync(t => t.TenantId == tenantId);

        public async Task<IEnumerable<Domain.Entities.Tenant>> GetAllAsync() =>
            await _context.Tenants.AsNoTracking().ToListAsync();

        public async Task AddAsync(Domain.Entities.Tenant tenant) =>
            await _context.Tenants.AddAsync(tenant);

        public void Update(Domain.Entities.Tenant tenant) =>
            _context.Tenants.Update(tenant);

        public void Delete(Domain.Entities.Tenant tenant) =>
            _context.Tenants.Remove(tenant);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
