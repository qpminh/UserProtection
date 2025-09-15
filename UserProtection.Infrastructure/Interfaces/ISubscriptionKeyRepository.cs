using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface ISubscriptionKeyRepository
{
    Task AddAsync(SubscriptionKey key);
    Task<SubscriptionKey?> GetByValueAsync(string keyValue);
    Task<IEnumerable<SubscriptionKey>> GetBySubscriptionIdAsync(int subscriptionId);
    Task SaveChangesAsync();
}