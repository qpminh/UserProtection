using UserProtection.Application.Dtos.Payments;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Payments
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto request);
        Task<PaymentResultDto?> HandleCallbackAsync(PaymentCallbackDto callback);
        Task<Payment?> GetByTransactionIdAsync(string txnId);
    }
}
