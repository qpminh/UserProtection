using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Infrastructure.Repositories.Subscriptions
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserProtectionContext _context;
        public SubscriptionRepository(UserProtectionContext context) => _context = context;

        public async Task AddAsync(Domain.Entities.Subscription subscription) =>
            await _context.Subscriptions.AddAsync(subscription);

        public async Task<Domain.Entities.Subscription?> GetByIdAsync(int id) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.SubscriptionId == id);

        public async Task<IEnumerable<Domain.Entities.Subscription>> GetByTenantAsync(int tenantId) =>
            await _context.Subscriptions
                .Where(s => s.TenantId == tenantId)
                .Include(s => s.Plan)
                .AsNoTracking()
                .ToListAsync();

        public void Update(Domain.Entities.Subscription subscription) =>
            _context.Subscriptions.Update(subscription);

        public void Delete(Domain.Entities.Subscription subscription) =>
            _context.Subscriptions.Remove(subscription);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
