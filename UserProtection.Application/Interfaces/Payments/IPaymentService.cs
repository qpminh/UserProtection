using UserProtection.Application.Dtos.Payments;
using UserProtection.Domain.Entities;

namespace UserProtection.Application.Interfaces.Payments
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto request);
        Task<PaymentResultDto?> HandleCallbackAsync(PaymentCallbackDto callback);
        Task<Payment?> GetByTransactionIdAsync(string txnId);
        Task<ManualPaymentResponseDto> CreateManualPaymentAsync(ManualPaymentRequestDto request);
        Task<ManualPaymentResponseDto?> UpdatePaymentStatusAsync(int paymentId, string newStatus);
        Task<ManualPaymentResponseDto?> UpdatePaymentAmountAsync(int paymentId, decimal newAmount);
        Task<IEnumerable<ManualPaymentResponseDto>> GetAllPaymentsAsync();
        Task<ManualPaymentResponseDto?> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<ManualPaymentResponseDto>> GetPaymentsByStatusAsync(string status);
    }
}
