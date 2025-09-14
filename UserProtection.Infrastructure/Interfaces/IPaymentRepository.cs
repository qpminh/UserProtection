using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Domain.Entities.Payment payment);
    Task<Domain.Entities.Payment?> GetByTransactionIdAsync(string transactionId);
    Task SaveChangesAsync();
}
