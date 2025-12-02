using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Infrastructure.Repositories.Subscriptions
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserProtectionContext _context;
        public SubscriptionRepository(UserProtectionContext context) => _context = context;

        public async Task AddAsync(Subscription subscription) =>
            await _context.Subscriptions.AddAsync(subscription);

        public void Update(Subscription subscription) =>
            _context.Subscriptions.Update(subscription);

        public void Delete(Subscription subscription) =>
            _context.Subscriptions.Remove(subscription);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<Subscription?> GetByIdAsync(int id) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                    .ThenInclude(p => p.PlanFeatures)
                        .ThenInclude(pf => pf.Feature)
                .Include(s => s.Payments)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SubscriptionId == id);

        public async Task<Subscription?> GetByIdForUpdateAsync(int id) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.SubscriptionId == id);

        public async Task<IEnumerable<Subscription>> GetAllAsync() =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                .Include(s => s.Payments)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<Subscription>> GetByTenantAsync(int tenantId) =>
            await _context.Subscriptions
                .Where(s => s.TenantId == tenantId)
                .Include(s => s.Plan)
                .Include(s => s.Payments)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Subscription?> GetActiveByUserAsync(string userId) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

        public async Task<Subscription?> GetLatestByUserAsync(string userId) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                    .ThenInclude(p => p.PlanFeatures)
                        .ThenInclude(pf => pf.Feature)
                .Include(s => s.Payments)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartDate)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Subscription>> GetByStatusAsync(string status) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                .Include(s => s.Payments)
                .Where(s => s.Status == status)
                .AsNoTracking()
                .ToListAsync();

        public async Task<IEnumerable<Subscription>> GetAllByUserAsync(string userId) =>
            await _context.Subscriptions
                .Include(s => s.Plan)
                    .ThenInclude(p => p.PlanFeatures)
                        .ThenInclude(pf => pf.Feature)
                .Include(s => s.Payments)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.StartDate)
                .AsNoTracking()
                .ToListAsync();
    }
}
