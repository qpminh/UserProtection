using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Payments;

namespace UserProtection.Infrastructure.Repositories.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly UserProtectionContext _context;
        public PaymentRepository(UserProtectionContext context) => _context = context;

        public async Task AddAsync(Payment payment) =>
            await _context.Payments.AddAsync(payment);

        public async Task<Payment?> GetByTransactionIdAsync(string transactionId) =>
            await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Plan)
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            return await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Plan)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Plan)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
        {
            return await _context.Payments
                .Include(p => p.Subscription)
                .ThenInclude(s => s.Plan)
                .Where(p => p.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
