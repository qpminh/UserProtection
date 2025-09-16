using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Subscriptions
{
    public interface ISubscriptionKeyService
    {
        Task<string> GenerateKeyAsync(int subscriptionId, bool deactivateOld = false);
        Task<Subscription?> ValidateKeyAsync(string keyValue);
        Task<IEnumerable<SubscriptionKey>> GetKeysAsync(int subscriptionId);
    }
}
