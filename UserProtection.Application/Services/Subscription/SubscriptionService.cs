using AutoMapper;
using UserProtection.Application.Dtos.Subscription;
using UserProtection.Domain.Constants;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Helpers;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Subscription;

public class SubscriptionService
{
    private readonly ISubscriptionRepository _subRepo;
    private readonly IMapper _mapper;

    public SubscriptionService(ISubscriptionRepository subRepo, IMapper mapper)
    {
        _subRepo = subRepo;
        _mapper = mapper;
    }

    public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionRequest request)
    {
        var sub = new Domain.Entities.Subscription
        {
            PlanId = request.PlanId,
            UserId = request.UserId,
            Status = SubscriptionStatus.Pending,
            StartDate = DateTime.UtcNow,
            AutoRenew = false
        };

        await _subRepo.AddAsync(sub);
        await _subRepo.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(sub);
    }
}
