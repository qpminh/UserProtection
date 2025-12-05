using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Payments;
using UserProtection.Infrastructure.Interfaces.Plans;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Application.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subRepo;
        private readonly IPlanRepository _planRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subRepo,
            IPlanRepository planRepo,
            IPaymentRepository paymentRepo,
            ICurrentUserService currentUserService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _subRepo = subRepo;
            _planRepo = planRepo;
            _paymentRepo = paymentRepo;
            _currentUserService = currentUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> CreatePendingSubscriptionAsync(int planId, string? userId = null)
        {
            var targetUserId = !string.IsNullOrWhiteSpace(userId)
                ? userId
                : _currentUserService.UserId;

            if (string.IsNullOrWhiteSpace(targetUserId))
                throw new UnauthorizedAccessException("User not authenticated.");

            var user = await _userManager.FindByIdAsync(targetUserId)
                ?? throw new Exception($"User not found with id: {targetUserId}");

            var plan = await _planRepo.GetByIdAsync(planId)
                ?? throw new Exception("Invalid plan.");

            var active = await _subRepo.GetActiveByUserAsync(targetUserId);
            if (active != null)
                throw new InvalidOperationException("User already has an active subscription.");

            var pending = await _subRepo.GetPendingByUserAsync(targetUserId);
            if (pending != null)
                throw new InvalidOperationException("User already has a pending subscription.");

            var sub = new Subscription
            {
                PlanId = plan.PlanId,
                UserId = targetUserId,
                Status = SubscriptionStatus.Pending,
                StartDate = DateTime.UtcNow,
                AutoRenew = false
            };

            await _subRepo.AddAsync(sub);
            await _subRepo.SaveChangesAsync();

            var payment = new Payment
            {
                SubscriptionId = sub.SubscriptionId,
                Amount = plan.Price,
                PaymentMethod = "Cash",
                Status = PaymentStatus.Pending,
                PaymentDate = DateTime.UtcNow,
                TransactionId = $"PENDING-{Guid.NewGuid():N}"
            };

            await _paymentRepo.AddAsync(payment);
            await _paymentRepo.SaveChangesAsync();

            sub.Payments = new List<Payment> { payment };

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetPendingAsync()
        {
            var subs = await _subRepo.GetByStatusAsync(SubscriptionStatus.Pending);
            return subs.Select(_mapper.Map<SubscriptionDto>);
        }

        public async Task<SubscriptionDto?> GetByIdAsync(int id)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            return sub == null ? null : _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
        {
            var subs = await _subRepo.GetAllAsync();
            return subs.Select(_mapper.Map<SubscriptionDto>);
        }

        public async Task<SubscriptionDto?> UpdateStatusAsync(int id, string status)
        {
            var allowed = new[] {
            SubscriptionStatus.Pending,
            SubscriptionStatus.Active,
            SubscriptionStatus.Cancelled,
            SubscriptionStatus.Expired
        };

            if (!allowed.Contains(status))
                throw new Exception("Invalid subscription status.");

            var sub = await _subRepo.GetByIdForUpdateAsync(id);
            if (sub == null)
                return null;

            if (status == SubscriptionStatus.Active)
            {
                var lastPayment = sub.Payments
                    .OrderByDescending(p => p.PaymentDate)
                    .FirstOrDefault();

                if (lastPayment == null || lastPayment.Status != PaymentStatus.Succeeded)
                    throw new Exception("Cannot activate subscription without a successful payment.");

                var plan = await _planRepo.GetByIdAsync(sub.PlanId);

                sub.StartDate = DateTime.UtcNow;
                sub.EndDate = plan?.BillingCycle.ToLower() switch
                {
                    "monthly" => sub.StartDate.AddMonths(1),
                    "yearly" => sub.StartDate.AddYears(1),
                    _ => sub.StartDate.AddMonths(1)
                };
            }

            if (status == SubscriptionStatus.Cancelled)
            {
                sub.AutoRenew = false;
                sub.EndDate = DateTime.UtcNow;
            }

            if (status == SubscriptionStatus.Expired)
            {
                sub.AutoRenew = false;
            }

            sub.Status = status;

            _subRepo.Update(sub);
            await _subRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<UserSubscriptionInfoDto?> GetCurrentUserSubscriptionAsync()
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
                throw new UnauthorizedAccessException("User not authenticated.");

            return await GetUserSubscriptionInfoAsync(userId);
        }

        public async Task<UserSubscriptionInfoDto?> GetUserSubscriptionInfoAsync(string userId)
        {
            var subscriptions = await _subRepo.GetAllByUserAsync(userId);
            if (subscriptions == null || !subscriptions.Any())
                return null;

            return new UserSubscriptionInfoDto
            {
                UserId = userId,
                Subscriptions = subscriptions.Select(s => new UserSubscriptionDetailDto
                {
                    SubscriptionId = s.SubscriptionId,
                    PlanName = s.Plan.Name,
                    PlanDescription = s.Plan.Description,
                    Status = s.Status,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Features = s.Plan.PlanFeatures.Select(f => f.Feature.Name).ToList(),
                    Payments = s.Payments.Select(p => new UserPaymentDto
                    {
                        PaymentId = p.PaymentId,
                        Amount = p.Amount,
                        PaymentMethod = p.PaymentMethod,
                        Status = p.Status,
                        PaymentDate = p.PaymentDate,
                        TransactionId = p.TransactionId
                    }).OrderByDescending(p => p.PaymentDate).ToList()
                })
                .OrderByDescending(s => s.StartDate)
                .ToList()
            };
        }

        public async Task<SubscriptionDto?> UpdateDatesAsync(int id, DateTime? startDate, DateTime? endDate)
        {
            var sub = await _subRepo.GetByIdForUpdateAsync(id);
            if (sub == null)
                return null;

            if (startDate.HasValue && endDate.HasValue && endDate <= startDate)
                throw new Exception("EndDate must be greater than StartDate.");

            if (startDate.HasValue)
                sub.StartDate = startDate.Value;

            if (endDate.HasValue)
                sub.EndDate = endDate.Value;

            _subRepo.Update(sub);
            await _subRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }
    }
}
