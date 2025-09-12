using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> AddAsync(Subscription subscription);
        Task<Subscription?> GetByIdAsync(int id);   
        Task SaveChangesAsync();
    }
}
