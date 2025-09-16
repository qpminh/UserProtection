using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Tenants
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(int tenantId);
        Task<IEnumerable<Tenant>> GetAllAsync();
        Task AddAsync(Tenant tenant);
        void Update(Tenant tenant);
        void Delete(Tenant tenant);
        Task SaveChangesAsync();
    }
}
