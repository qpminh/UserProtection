using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly UserProtectionContext _context;
    public CourseRepository(UserProtectionContext context) => _context = context;

    public async Task<IEnumerable<Course>> GetAllAsync() =>
        await _context.Courses.ToListAsync();

    public async Task<Course?> GetByIdAsync(int id) =>
        await _context.Courses.FindAsync(id);

    public async Task AddAsync(Course entity) =>
        await _context.Courses.AddAsync(entity);

    public Task UpdateAsync(Course entity)
    {
        _context.Courses.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Course entity)
    {
        _context.Courses.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}