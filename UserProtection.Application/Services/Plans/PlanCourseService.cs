using UserProtection.Application.Interfaces.Plans;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Plans;

namespace UserProtection.Application.Services.Plans
{
    public class PlanCourseService : IPlanCourseService
    {
        private readonly IPlanCourseRepository _repo;
        public PlanCourseService(IPlanCourseRepository repo) => _repo = repo;

        public async Task AddCourseToPlanAsync(int planId, int courseId)
        {
            if (await _repo.GetAsync(planId, courseId) != null) return;
            await _repo.AddAsync(new PlanCourse { PlanId = planId, CourseId = courseId });
            await _repo.SaveChangesAsync();
        }

        public async Task RemoveCourseFromPlanAsync(int planId, int courseId)
        {
            var pc = await _repo.GetAsync(planId, courseId);
            if (pc == null) return;
            await _repo.RemoveAsync(pc);
            await _repo.SaveChangesAsync();
        }
    }
}