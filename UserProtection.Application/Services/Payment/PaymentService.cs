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

    public PaymentService(IPaymentRepository paymentRepo, ISubscriptionRepository subRepo, VnPayHelper vnPay)
    {
        _paymentRepo = paymentRepo;
        _subRepo = subRepo;
        _vnPay = vnPay;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto request)
    {
        var subscription = await _subRepo.GetByIdAsync(request.SubscriptionId);
        if (subscription == null || subscription.Plan == null)
            throw new Exception("Invalid subscription or plan");

        var amount = subscription.Plan.Price;
        var transactionId = Guid.NewGuid().ToString("N");

        var payment = new Domain.Entities.Payment
        {
            SubscriptionId = subscription.SubscriptionId,
            Amount = amount,
            PaymentMethod = "VNPay",
            Status = PaymentStatus.Pending,
            PaymentDate = DateTime.UtcNow,
            TransactionId = transactionId,
            FrontendReturnUrl = request.ReturnUrl
        };

        await _paymentRepo.AddAsync(payment);
        await _paymentRepo.SaveChangesAsync();

        var paymentUrl = _vnPay.CreatePaymentUrl(transactionId, payment.SubscriptionId, payment.Amount);

        return new PaymentResponseDto { PaymentUrl = paymentUrl };
    }

    public async Task<bool> HandleCallbackAsync(PaymentCallbackDto callback)
    {
        var payment = await _paymentRepo.GetByTransactionIdAsync(callback.TransactionId);
        if (payment == null) return false;

        var subscription = payment.Subscription;
        if (subscription == null) return false;

        if (callback.Status == "Success")
        {
            payment.Status = PaymentStatus.Succeeded;
            subscription.Status = SubscriptionStatus.Active;
            subscription.StartDate = DateTime.UtcNow;

            // Tính EndDate theo BillingCycle
            subscription.EndDate = subscription.Plan.BillingCycle.ToLower() switch
            {
                "monthly" => subscription.StartDate.AddMonths(1),
                "yearly" => subscription.StartDate.AddYears(1),
                _ => subscription.StartDate.AddMonths(1) // default: monthly
            };
        }
        else
        {
            payment.Status = PaymentStatus.Failed;
            subscription.Status = SubscriptionStatus.Pending;
        }

        await _paymentRepo.SaveChangesAsync();
        return true;
    }

    public async Task<Domain.Entities.Payment?> GetByTransactionIdAsync(string txnId)
    {
        return await _paymentRepo.GetByTransactionIdAsync(txnId);
    }
}