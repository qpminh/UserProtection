using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Payment;

public class PlanRepository : IPlanRepository
{
    private readonly UserProtectionContext _context;

    public PlanRepository(UserProtectionContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plan>> GetAllPlansAsync()
    {
        return await _context.Plans
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<Plan?> GetPlanWithCoursesAsync(int planId)
    {
        return await _context.Plans
            .Include(p => p.PlanCourses)
                .ThenInclude(pc => pc.Course)
            .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive);
    }

    public async Task<Plan?> GetPlanWithFeaturesAsync(int planId)
    {
        return await _context.Plans
            .Include(p => p.PlanFeatures)
                .ThenInclude(pf => pf.Feature)
            .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive);
    }
}
