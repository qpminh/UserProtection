using AutoMapper;
using UserProtection.Application.Dtos;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Payment;

public class PlanService
{
    private readonly IPlanRepository _repo;
    private readonly IMapper _mapper;
    public PlanService(IPlanRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<IEnumerable<PlanDto>> GetActivePlansAsync() =>
        _mapper.Map<IEnumerable<PlanDto>>(await _repo.GetActivePlansAsync());

    public async Task<IEnumerable<PlanDto>> GetAllPlansAsync() =>
    _mapper.Map<IEnumerable<PlanDto>>(await _repo.GetAllPlansAsync());

    public async Task<PlanDto?> GetPlanWithCoursesAsync(int planId) =>
        _mapper.Map<PlanDto?>(await _repo.GetPlanWithCoursesAsync(planId));

    public async Task<PlanDto?> GetPlanWithFeaturesAsync(int planId) =>
        _mapper.Map<PlanDto?>(await _repo.GetPlanWithFeaturesAsync(planId));

    public async Task<PlanDto?> GetPlanDetailsAsync(int planId) =>
        _mapper.Map<PlanDto?>(await _repo.GetPlanDetailsAsync(planId));
}