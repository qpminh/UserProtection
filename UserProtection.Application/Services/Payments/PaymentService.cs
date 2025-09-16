using UserProtection.Application.Dtos.Payment;
using UserProtection.Application.Interfaces.Payments;
using UserProtection.Application.Services.Subscriptions;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Helpers;
using UserProtection.Infrastructure.Interfaces.Payments;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Application.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly ISubscriptionRepository _subRepo;
        private readonly SubscriptionKeyService _keyService;
        private readonly VnPayHelper _vnPay;

        public PaymentService(
            IPaymentRepository paymentRepo,
            ISubscriptionRepository subRepo,
            SubscriptionKeyService keyService,
            VnPayHelper vnPay)
        {
            _paymentRepo = paymentRepo;
            _subRepo = subRepo;
            _keyService = keyService;
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

        public async Task<PaymentResultDto?> HandleCallbackAsync(PaymentCallbackDto callback)
        {
            var payment = await _paymentRepo.GetByTransactionIdAsync(callback.TransactionId);
            if (payment == null) return null;

            var subscription = payment.Subscription;
            if (subscription == null) return null;

            string? apiKey = null;

            if (callback.Status == "Success")
            {
                payment.Status = PaymentStatus.Succeeded;
                subscription.Status = SubscriptionStatus.Active;
                subscription.StartDate = DateTime.UtcNow;

                subscription.EndDate = subscription.Plan.BillingCycle.ToLower() switch
                {
                    "monthly" => subscription.StartDate.AddMonths(1),
                    "yearly" => subscription.StartDate.AddYears(1),
                    _ => subscription.StartDate.AddMonths(1)
                };

                apiKey = await _keyService.GenerateKeyAsync(subscription.SubscriptionId, deactivateOld: true);
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                subscription.Status = SubscriptionStatus.Pending;
            }

            await _paymentRepo.SaveChangesAsync();

            return new PaymentResultDto
            {
                TransactionId = callback.TransactionId,
                SubscriptionId = subscription.SubscriptionId,
                Status = callback.Status,
                ApiKey = apiKey
            };
        }

        public async Task<Payment?> GetByTransactionIdAsync(string txnId)
        {
            return await _paymentRepo.GetByTransactionIdAsync(txnId);
        }
    }
}