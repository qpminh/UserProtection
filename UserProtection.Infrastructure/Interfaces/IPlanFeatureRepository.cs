using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPlanFeatureRepository
{
    Task AddAsync(PlanFeature planFeature);
    Task RemoveAsync(PlanFeature planFeature);
    Task<PlanFeature?> GetAsync(int planId, int featureId);
    Task SaveChangesAsync();
}