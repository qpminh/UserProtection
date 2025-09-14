using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Payment;

public class PaymentRepository : IPaymentRepository
{
    private readonly UserProtectionContext _context;
    public PaymentRepository(UserProtectionContext context) => _context = context;

    public async Task AddAsync(Domain.Entities.Payment payment) =>
        await _context.Payments.AddAsync(payment);

    public async Task<Domain.Entities.Payment?> GetByTransactionIdAsync(string transactionId) =>
        await _context.Payments
            .Include(p => p.Subscription).ThenInclude(s => s.Plan) // cần Plan để tính EndDate
            .FirstOrDefaultAsync(p => p.TransactionId == transactionId);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}