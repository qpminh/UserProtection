using UserProtection.Application.Dtos.Subscriptions;

namespace UserProtection.Application.Interfaces.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest request, int tenantId);
        Task<SubscriptionDto?> GetByIdAsync(int id);
        Task<IEnumerable<SubscriptionDto>> GetByTenantAsync(int tenantId);
        Task<SubscriptionDto?> UpdateAsync(int id, UpdateSubscriptionRequest dto);
        Task<bool> CancelAsync(int id);
    }
}
