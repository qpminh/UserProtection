using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Payments
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment?> GetByTransactionIdAsync(string transactionId);
        Task SaveChangesAsync();
    }
}