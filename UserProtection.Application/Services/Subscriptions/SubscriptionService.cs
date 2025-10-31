using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Plans;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Application.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subRepo;
        private readonly IPlanRepository _planRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subRepo,
            IPlanRepository planRepo,
            ICurrentUserService currentUserService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _subRepo = subRepo;
            _planRepo = planRepo;
            _currentUserService = currentUserService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> CreatePendingSubscriptionAsync(int planId, string userId)
        {
            var plan = await _planRepo.GetByIdAsync(planId)
                ?? throw new Exception("Invalid plan.");

            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new Exception("User not found.");

            var existing = await _subRepo.GetActiveByUserAsync(userId);
            if (existing != null)
                throw new InvalidOperationException("User already has an active subscription.");

            var sub = new Subscription
            {
                PlanId = plan.PlanId,
                UserId = userId,
                Status = SubscriptionStatus.Pending,
                StartDate = DateTime.UtcNow,
                AutoRenew = false
            };

            await _subRepo.AddAsync(sub);
            await _subRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetPendingAsync()
        {
            var subs = await _subRepo.GetByStatusAsync(SubscriptionStatus.Pending);
            return subs.Select(s => _mapper.Map<SubscriptionDto>(s));
        }

        public async Task<SubscriptionDto?> GetByIdAsync(int id)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            return sub == null ? null : _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
        {
            var subs = await _subRepo.GetAllAsync();
            return subs.Select(s => _mapper.Map<SubscriptionDto>(s));
        }

        public async Task<SubscriptionDto?> UpdateStatusAsync(int id, string status)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            if (sub == null) return null;

            sub.Status = status;

            // Nếu được kích hoạt, set ngày bắt đầu & hết hạn
            if (status.Equals(SubscriptionStatus.Active, StringComparison.OrdinalIgnoreCase))
            {
                var plan = await _planRepo.GetByIdAsync(sub.PlanId);
                sub.StartDate = DateTime.UtcNow;
                sub.EndDate = plan?.BillingCycle.ToLower() switch
                {
                    "monthly" => sub.StartDate.AddMonths(1),
                    "yearly" => sub.StartDate.AddYears(1),
                    _ => sub.StartDate.AddMonths(1)
                };
            }

            _subRepo.Update(sub);
            await _subRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<UserSubscriptionInfoDto?> GetUserSubscriptionInfoAsync(string userId)
        {
            var subscription = await _subRepo.GetLatestByUserAsync(userId);
            if (subscription == null) return null;

            return new UserSubscriptionInfoDto
            {
                UserId = userId,
                PlanName = subscription.Plan.Name,
                PlanDescription = subscription.Plan.Description,
                Status = subscription.Status,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                Features = subscription.Plan.PlanFeatures.Select(f => f.Feature.Name).ToList()
            };
        }

        public async Task<UserSubscriptionInfoDto?> GetCurrentUserSubscriptionAsync()
        {
            var userId = _currentUserService.UserId;
            if (userId == null)
                throw new UnauthorizedAccessException("User not authenticated.");

            return await GetUserSubscriptionInfoAsync(userId);
        }
    }
}
