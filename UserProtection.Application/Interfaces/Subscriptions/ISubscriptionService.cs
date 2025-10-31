using UserProtection.Application.Dtos.Subscriptions;

namespace UserProtection.Application.Interfaces.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> CreatePendingSubscriptionAsync(int planId, string userId);
        Task<IEnumerable<SubscriptionDto>> GetPendingAsync();
        Task<SubscriptionDto?> GetByIdAsync(int id);
        Task<IEnumerable<SubscriptionDto>> GetAllAsync();
        Task<SubscriptionDto?> UpdateStatusAsync(int id, string status);
        Task<UserSubscriptionInfoDto?> GetUserSubscriptionInfoAsync(string userId);
        Task<UserSubscriptionInfoDto?> GetCurrentUserSubscriptionAsync();
    }
}
