using AutoMapper;
using UserProtection.Application.DTOs;
using UserProtection.Domain.Constants;
using UserProtection.Infrastructure.Helpers;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Payment;

public class PaymentService
{
    private readonly IPaymentRepository _paymentRepo;
    private readonly ISubscriptionRepository _subRepo;
    private readonly VnPayHelper _vnPay;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepo, ISubscriptionRepository subRepo, VnPayHelper vnPay, IMapper mapper)
    {
        _paymentRepo = paymentRepo;
        _subRepo = subRepo;
        _vnPay = vnPay;
        _mapper = mapper;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto request)
    {
        var transactionId = Guid.NewGuid().ToString("N");

        var payment = new Domain.Entities.Payment
        {
            SubscriptionId = request.SubscriptionId,
            Amount = request.Amount,
            PaymentMethod = "VNPay",
            Status = PaymentStatus.Pending,
            PaymentDate = DateTime.UtcNow,
            TransactionId = transactionId
        };

        await _paymentRepo.AddAsync(payment);
        await _paymentRepo.SaveChangesAsync();

        // ✅ Truyền thêm subscriptionId vào URL (OrderInfo sẽ chứa ID này)
        var paymentUrl = _vnPay.CreatePaymentUrl(
            transactionId,
            payment.SubscriptionId,
            payment.Amount,
            request.ReturnUrl
        );

        return new PaymentResponseDto { PaymentUrl = paymentUrl };
    }

    public async Task HandleCallbackAsync(PaymentCallbackDto callback)
    {
        var payment = await _paymentRepo.GetByTransactionIdAsync(callback.TransactionId);
        if (payment == null) throw new Exception("Payment not found");

        var subscription = await _subRepo.GetByIdAsync(payment.SubscriptionId);
        if (subscription == null) throw new Exception("Subscription not found");

        if (callback.Status == "Success")
        {
            payment.Status = PaymentStatus.Succeeded;
            subscription.Status = SubscriptionStatus.Active;
        }
        else
        {
            payment.Status = PaymentStatus.Failed;
            subscription.Status = SubscriptionStatus.Pending;
        }

        await _paymentRepo.SaveChangesAsync();
    }
}
