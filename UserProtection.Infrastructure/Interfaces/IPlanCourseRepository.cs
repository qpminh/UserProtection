using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces;

public interface IPlanCourseRepository
{
    Task<PlanCourse?> GetAsync(int planId, int courseId);
    Task AddAsync(PlanCourse entity);
    Task RemoveAsync(PlanCourse entity);
    Task SaveChangesAsync();
}