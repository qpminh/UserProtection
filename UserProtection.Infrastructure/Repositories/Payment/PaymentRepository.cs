using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Payment;

public class PaymentRepository : IPaymentRepository
{
    private readonly UserProtectionContext _context;

    public PaymentRepository(UserProtectionContext context)
    {
        _context = context;
    }

    public async Task<UserProtection.Domain.Entities.Payment> AddAsync(UserProtection.Domain.Entities.Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        return payment;
    }

    public async Task<UserProtection.Domain.Entities.Payment?> GetByTransactionIdAsync(string transactionId)
    {
        return await _context.Payments
            .Include(p => p.Subscription) // để load Subscription luôn (dùng cho HandleCallback)
            .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
