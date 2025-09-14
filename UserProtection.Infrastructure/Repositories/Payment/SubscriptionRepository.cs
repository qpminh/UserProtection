using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Payment;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly UserProtectionContext _context;
    public SubscriptionRepository(UserProtectionContext context) => _context = context;

    public async Task AddAsync(Subscription subscription) =>
        await _context.Subscriptions.AddAsync(subscription);

    public async Task<Subscription?> GetByIdAsync(int id) =>
        await _context.Subscriptions
            .Include(s => s.Plan) // include Plan để tính EndDate
            .FirstOrDefaultAsync(s => s.SubscriptionId == id);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}