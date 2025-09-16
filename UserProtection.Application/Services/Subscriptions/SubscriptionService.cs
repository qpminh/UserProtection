using AutoMapper;
using UserProtection.Application.Dtos.Subscriptions;
using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Subscriptions;
using UserProtection.Infrastructure.Interfaces.Tenants;

namespace UserProtection.Application.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subRepo;
        private readonly ITenantUserAccessRepository _tenantUserAccessRepo;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subRepo,
            ITenantUserAccessRepository tenantUserAccessRepo,
            IMapper mapper)
        {
            _subRepo = subRepo;
            _tenantUserAccessRepo = tenantUserAccessRepo;
            _mapper = mapper;
        }

        public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest request, int tenantId)
        {
            // 1. Tạo Subscription mới
            var sub = new Domain.Entities.Subscription
            {
                PlanId = request.PlanId,
                TenantId = tenantId,
                UserId = request.UserId,
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow,
                AutoRenew = false
            };

            await _subRepo.AddAsync(sub);
            await _subRepo.SaveChangesAsync();

            // 2. Tạo TenantUserAccess cho User
            var access = new TenantUserAccess
            {
                TenantId = tenantId,
                UserId = request.UserId,
                SubscriptionId = sub.SubscriptionId,
                AssignedAt = DateTime.UtcNow,
                Status = "Active"
            };

            await _tenantUserAccessRepo.AddAsync(access);
            await _tenantUserAccessRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<SubscriptionDto?> GetByIdAsync(int id)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            return sub == null ? null : _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<IEnumerable<SubscriptionDto>> GetByTenantAsync(int tenantId)
        {
            var list = await _subRepo.GetByTenantAsync(tenantId);
            return _mapper.Map<IEnumerable<SubscriptionDto>>(list);
        }

        public async Task<SubscriptionDto?> UpdateAsync(int id, UpdateSubscriptionRequest dto)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            if (sub == null) return null;

            if (dto.AutoRenew.HasValue) sub.AutoRenew = dto.AutoRenew.Value;
            if (!string.IsNullOrEmpty(dto.Status)) sub.Status = dto.Status;
            if (dto.EndDate.HasValue) sub.EndDate = dto.EndDate;

            _subRepo.Update(sub);
            await _subRepo.SaveChangesAsync();

            return _mapper.Map<SubscriptionDto>(sub);
        }

        public async Task<bool> CancelAsync(int id)
        {
            var sub = await _subRepo.GetByIdAsync(id);
            if (sub == null) return false;

            sub.Status = "Cancelled";
            sub.EndDate = DateTime.UtcNow;
            _subRepo.Update(sub);
            await _subRepo.SaveChangesAsync();
            return true;
        }
    }
}
