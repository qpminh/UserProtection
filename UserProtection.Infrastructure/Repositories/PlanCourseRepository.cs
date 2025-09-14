using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class PlanCourseRepository : IPlanCourseRepository
{
    private readonly UserProtectionContext _context;
    public PlanCourseRepository(UserProtectionContext context) => _context = context;

    public async Task<PlanCourse?> GetAsync(int planId, int courseId) =>
        await _context.PlanCourses
            .FirstOrDefaultAsync(pc => pc.PlanId == planId && pc.CourseId == courseId);

    public async Task AddAsync(PlanCourse entity) =>
        await _context.PlanCourses.AddAsync(entity);

    public Task RemoveAsync(PlanCourse entity)
    {
        _context.PlanCourses.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}