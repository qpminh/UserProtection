using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Tenants
{
    public interface ITenantUserAccessRepository
    {
        Task<TenantUserAccess?> GetByIdAsync(int accessId);
        Task<IEnumerable<TenantUserAccess>> GetByTenantAsync(int tenantId);
        Task<IEnumerable<TenantUserAccess>> GetByUserAsync(string userId);
        Task AddAsync(TenantUserAccess entity);
        void Update(TenantUserAccess entity);
        void Delete(TenantUserAccess entity);
        Task SaveChangesAsync();
    }
}
