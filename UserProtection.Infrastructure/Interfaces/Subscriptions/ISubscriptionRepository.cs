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
        Task<Subscription?> GetLatestByUserAsync(string userId);
        Task<Subscription?> GetActiveByUserAsync(string userId);
        Task<IEnumerable<Subscription>> GetByStatusAsync(string status);
        Task<IEnumerable<Subscription>> GetAllByUserAsync(string userId);
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription?> GetByIdForUpdateAsync(int id);
    }
}
