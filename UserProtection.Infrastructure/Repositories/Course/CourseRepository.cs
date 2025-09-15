using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories.Course;

public class CourseRepository : ICourseRepository
{
    private readonly UserProtectionContext _context;
    public CourseRepository(UserProtectionContext context) => _context = context;

    public async Task<IEnumerable<Domain.Entities.Course>> GetAllAsync() =>
        await _context.Courses.ToListAsync();

    public async Task<Domain.Entities.Course?> GetByIdAsync(int id) =>
        await _context.Courses.FindAsync(id);

    public async Task AddAsync(Domain.Entities.Course entity) =>
        await _context.Courses.AddAsync(entity);

    public Task UpdateAsync(Domain.Entities.Course entity)
    {
        _context.Courses.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Domain.Entities.Course entity)
    {
        _context.Courses.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}