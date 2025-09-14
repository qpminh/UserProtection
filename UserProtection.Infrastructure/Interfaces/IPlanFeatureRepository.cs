using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPlanFeatureRepository
{
    Task<PlanFeature?> GetAsync(int planId, int featureId);
    Task AddAsync(PlanFeature entity);
    Task RemoveAsync(PlanFeature entity);
    Task SaveChangesAsync();
}