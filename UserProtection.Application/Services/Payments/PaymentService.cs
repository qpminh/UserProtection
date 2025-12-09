using UserProtection.Application.Dtos.Payments;
using UserProtection.Application.Interfaces.Payments;
using UserProtection.Application.Interfaces.Subscriptions;
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
        private readonly ISubscriptionKeyService _keyService;
        private readonly VnPayHelper _vnPay;

        public PaymentService(
            IPaymentRepository paymentRepo,
            ISubscriptionRepository subRepo,
            ISubscriptionKeyService keyService,
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
                PaymentMethod = "Bank",
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

        public async Task<ManualPaymentResponseDto> CreateManualPaymentAsync(ManualPaymentRequestDto request)
        {
            var subscription = await _subRepo.GetByIdAsync(request.SubscriptionId);
            if (subscription == null)
                throw new Exception($"Subscription {request.SubscriptionId} not found.");

            var payment = new Payment
            {
                SubscriptionId = subscription.SubscriptionId,
                Amount = request.Amount,
                //PaymentMethod = request.PaymentMethod,
                PaymentMethod = "Bank",
                Status = request.Status,
                PaymentDate = DateTime.UtcNow,
                TransactionId = request.TransactionId ?? $"MANUAL-{Guid.NewGuid():N}",
                FrontendReturnUrl = null
            };

            await _paymentRepo.AddAsync(payment);

            if (request.Status.Equals(PaymentStatus.Succeeded, StringComparison.OrdinalIgnoreCase))
            {
                subscription.Status = SubscriptionStatus.Active;
                subscription.StartDate = DateTime.UtcNow;
                subscription.EndDate = subscription.Plan.BillingCycle.ToLower() switch
                {
                    "monthly" => subscription.StartDate.AddMonths(1),
                    "yearly" => subscription.StartDate.AddYears(1),
                    _ => subscription.StartDate.AddMonths(1)
                };

                await _keyService.GenerateKeyAsync(subscription.SubscriptionId, deactivateOld: true);
            }

            await _paymentRepo.SaveChangesAsync();

            return new ManualPaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                SubscriptionId = payment.SubscriptionId,
                Amount = payment.Amount,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId
            };
        }

        public async Task<ManualPaymentResponseDto?> UpdatePaymentStatusAsync(int paymentId, string newStatus)
        {
            var payment = await _paymentRepo.GetByIdAsync(paymentId);
            if (payment == null) return null;

            payment.Status = newStatus;
            payment.PaymentDate = DateTime.UtcNow;

            var subscription = payment.Subscription;

            if (newStatus.Equals(PaymentStatus.Succeeded, StringComparison.OrdinalIgnoreCase))
            {
                subscription.Status = SubscriptionStatus.Active;
                subscription.StartDate = DateTime.UtcNow;
                subscription.EndDate = subscription.Plan.BillingCycle.ToLower() switch
                {
                    "monthly" => subscription.StartDate.AddMonths(1),
                    "yearly" => subscription.StartDate.AddYears(1),
                    _ => subscription.StartDate.AddMonths(1)
                };

                await _keyService.GenerateKeyAsync(subscription.SubscriptionId, deactivateOld: true);
            }
            else if (newStatus.Equals(PaymentStatus.Failed, StringComparison.OrdinalIgnoreCase))
            {
                subscription.Status = SubscriptionStatus.Pending;
            }

            await _paymentRepo.SaveChangesAsync();

            return new ManualPaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                SubscriptionId = payment.SubscriptionId,
                Amount = payment.Amount,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId
            };
        }

        public async Task<ManualPaymentResponseDto?> UpdatePaymentAmountAsync(int paymentId, decimal newAmount)
        {
            var payment = await _paymentRepo.GetByIdAsync(paymentId);
            if (payment == null) return null;

            payment.Amount = newAmount;
            payment.PaymentDate = DateTime.UtcNow;

            await _paymentRepo.SaveChangesAsync();

            return new ManualPaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                SubscriptionId = payment.SubscriptionId,
                Amount = payment.Amount,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId
            };
        }

        public async Task<IEnumerable<PaymentWithUserDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepo.GetAllAsync();

            return payments.Select(p => new PaymentWithUserDto
            {
                PaymentId = p.PaymentId,
                SubscriptionId = p.SubscriptionId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                TransactionId = p.TransactionId,
                UserId = p.Subscription?.User?.Id ?? "",
                UserEmail = p.Subscription?.User?.Email ?? "",
                FullName = $"{p.Subscription?.User?.FirstName} {p.Subscription?.User?.LastName}".Trim()
            });
        }

        public async Task<IEnumerable<PaymentWithUserDto>> GetPaymentsByStatusAsync(string status)
        {
            var payments = await _paymentRepo.GetByStatusAsync(status);

            return payments.Select(p => new PaymentWithUserDto
            {
                PaymentId = p.PaymentId,
                SubscriptionId = p.SubscriptionId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                TransactionId = p.TransactionId,
                UserId = p.Subscription?.User?.Id ?? "",
                UserEmail = p.Subscription?.User?.Email ?? "",
                FullName = $"{p.Subscription?.User?.FirstName} {p.Subscription?.User?.LastName}".Trim()
            });
        }

        public async Task<PaymentWithUserDto?> GetPaymentByIdAsync(int paymentId)
        {
            var p = await _paymentRepo.GetByIdAsync(paymentId);
            if (p == null) return null;

            return new PaymentWithUserDto
            {
                PaymentId = p.PaymentId,
                SubscriptionId = p.SubscriptionId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                TransactionId = p.TransactionId,
                UserId = p.Subscription?.User?.Id ?? "",
                UserEmail = p.Subscription?.User?.Email ?? "",
                FullName = $"{p.Subscription?.User?.FirstName} {p.Subscription?.User?.LastName}".Trim()
            };
        }

        public async Task<PaymentWithUserDto?> GetByTransactionIdDetailedAsync(string txnId)
        {
            var p = await _paymentRepo.GetByTransactionIdAsync(txnId);
            if (p == null) return null;

            return new PaymentWithUserDto
            {
                PaymentId = p.PaymentId,
                SubscriptionId = p.SubscriptionId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                TransactionId = p.TransactionId,
                UserId = p.Subscription?.User?.Id ?? "",
                UserEmail = p.Subscription?.User?.Email ?? "",
                FullName = $"{p.Subscription?.User?.FirstName} {p.Subscription?.User?.LastName}".Trim()
            };
        }
    }
}