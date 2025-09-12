using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPaymentRepository
{
    Task<Payment> AddAsync(Payment payment);
    Task<Payment?> GetByTransactionIdAsync(string transactionId);
    Task SaveChangesAsync();
}
