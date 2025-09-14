using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class PlanFeatureRepository : IPlanFeatureRepository
{
    private readonly UserProtectionContext _context;
    public PlanFeatureRepository(UserProtectionContext context) => _context = context;

    public async Task<PlanFeature?> GetAsync(int planId, int featureId) =>
        await _context.PlanFeatures
            .FirstOrDefaultAsync(pf => pf.PlanId == planId && pf.FeatureId == featureId);

    public async Task AddAsync(PlanFeature entity) =>
        await _context.PlanFeatures.AddAsync(entity);

    public Task RemoveAsync(PlanFeature entity)
    {
        _context.PlanFeatures.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}