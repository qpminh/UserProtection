using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class PlanFeatureRepository : IPlanFeatureRepository
{
    private readonly UserProtectionContext _context;
    public PlanFeatureRepository(UserProtectionContext context) => _context = context;

    public async Task AddAsync(PlanFeature planFeature) => await _context.PlanFeatures.AddAsync(planFeature);
    public async Task RemoveAsync(PlanFeature planFeature) => _context.PlanFeatures.Remove(planFeature);
    public async Task<PlanFeature?> GetAsync(int planId, int featureId) =>
        await _context.PlanFeatures.FirstOrDefaultAsync(pf => pf.PlanId == planId && pf.FeatureId == featureId);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}