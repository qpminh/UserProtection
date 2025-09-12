using AutoMapper;
using UserProtection.Application.Dtos;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Payment;

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
        var sub = _mapper.Map<Subscription>(request);
        await _subRepo.AddAsync(sub);
        await _subRepo.SaveChangesAsync();

        return _mapper.Map<SubscriptionDto>(sub);
    }
}