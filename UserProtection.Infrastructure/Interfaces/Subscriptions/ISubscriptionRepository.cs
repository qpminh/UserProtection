using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(int id);
        Task<IEnumerable<Subscription>> GetByTenantAsync(int tenantId);
        Task AddAsync(Subscription subscription);
        void Update(Subscription subscription);
        void Delete(Subscription subscription);
        Task SaveChangesAsync();
    }
}
