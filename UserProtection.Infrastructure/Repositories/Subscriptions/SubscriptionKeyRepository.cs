using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Infrastructure.Repositories.Subscriptions
{
    public class SubscriptionKeyRepository : ISubscriptionKeyRepository
    {
        private readonly UserProtectionContext _context;
        public SubscriptionKeyRepository(UserProtectionContext context) => _context = context;

        public async Task AddAsync(SubscriptionKey key) =>
            await _context.SubscriptionKeys.AddAsync(key);

        public async Task<SubscriptionKey?> GetByValueAsync(string keyValue) =>
            await _context.SubscriptionKeys
                .Include(k => k.Subscription)
                .ThenInclude(s => s.Plan)
                .FirstOrDefaultAsync(k => k.KeyValue == keyValue && k.IsActive);

        public async Task<IEnumerable<SubscriptionKey>> GetBySubscriptionIdAsync(int subscriptionId) =>
            await _context.SubscriptionKeys
                .Where(k => k.SubscriptionId == subscriptionId)
                .ToListAsync();

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}