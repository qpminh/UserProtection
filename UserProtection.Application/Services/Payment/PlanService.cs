using AutoMapper;
using UserProtection.Application.Dtos;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Payment;

public class PlanService
{
    private readonly IPlanRepository _planRepo;
    private readonly IMapper _mapper;

    public PlanService(IPlanRepository planRepo, IMapper mapper)
    {
        _planRepo = planRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
    {
        var plans = await _planRepo.GetAllPlansAsync();
        return _mapper.Map<IEnumerable<PlanDto>>(plans);
    }

    public async Task<PlanDto?> GetPlanWithCoursesAsync(int planId)
    {
        var plan = await _planRepo.GetPlanWithCoursesAsync(planId);
        return _mapper.Map<PlanDto?>(plan);
    }

    public async Task<PlanDto?> GetPlanWithFeaturesAsync(int planId)
    {
        var plan = await _planRepo.GetPlanWithFeaturesAsync(planId);
        return _mapper.Map<PlanDto?>(plan);
    }
}
