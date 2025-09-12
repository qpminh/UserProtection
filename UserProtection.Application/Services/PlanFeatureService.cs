using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services;

public class PlanFeatureService
{
    private readonly IPlanFeatureRepository _repo;

    public PlanFeatureService(IPlanFeatureRepository repo) => _repo = repo;

    public async Task AddFeatureToPlanAsync(int planId, int featureId)
    {
        var existing = await _repo.GetAsync(planId, featureId);
        if (existing != null) return;

        await _repo.AddAsync(new PlanFeature { PlanId = planId, FeatureId = featureId });
        await _repo.SaveChangesAsync();
    }

    public async Task RemoveFeatureFromPlanAsync(int planId, int featureId)
    {
        var pf = await _repo.GetAsync(planId, featureId);
        if (pf == null) return;

        await Task.Run(() => _repo.RemoveAsync(pf));
        await _repo.SaveChangesAsync();
    }
}