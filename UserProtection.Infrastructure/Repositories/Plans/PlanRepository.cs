using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Plans;

namespace UserProtection.Infrastructure.Repositories.Plans
{
    public class PlanRepository : IPlanRepository
    {
        private readonly UserProtectionContext _context;
        public PlanRepository(UserProtectionContext context) => _context = context;

        public async Task<IEnumerable<Domain.Entities.Plan>> GetActivePlansAsync() =>
            await _context.Plans.Where(p => p.IsActive)
                .Include(p => p.PlanCourses).ThenInclude(pc => pc.Course)
                .Include(p => p.PlanFeatures).ThenInclude(pf => pf.Feature).ToListAsync();

        public async Task<IEnumerable<Domain.Entities.Plan>> GetAllPlansAsync() =>
        await _context.Plans
            .Include(p => p.PlanCourses).ThenInclude(pc => pc.Course)
            .Include(p => p.PlanFeatures).ThenInclude(pf => pf.Feature).ToListAsync();

        public async Task<Domain.Entities.Plan?> GetPlanWithCoursesAsync(int planId) =>
            await _context.Plans
                .Include(p => p.PlanCourses).ThenInclude(pc => pc.Course)
                .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive);

        public async Task<Domain.Entities.Plan?> GetPlanWithFeaturesAsync(int planId) =>
            await _context.Plans
                .Include(p => p.PlanFeatures).ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive);

        public async Task<Domain.Entities.Plan?> GetPlanDetailsAsync(int planId) =>
            await _context.Plans
                .Include(p => p.PlanCourses).ThenInclude(pc => pc.Course)
                .Include(p => p.PlanFeatures).ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(p => p.PlanId == planId && p.IsActive);
    }
}