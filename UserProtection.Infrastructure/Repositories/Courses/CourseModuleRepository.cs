using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Infrastructure.Repositories.Courses
{
    public class CourseModuleRepository : ICourseModuleRepository
    {
        private readonly UserProtectionContext _context;
        public CourseModuleRepository(UserProtectionContext context) => _context = context;

        public async Task<IEnumerable<CourseModule>> GetByCourseAsync(int courseId) =>
            await _context.CourseModules
                .Where(m => m.CourseId == courseId)
                .OrderBy(m => m.OrderIndex)
                .ToListAsync();

        public async Task<CourseModule?> GetByIdAsync(int id) =>
            await _context.CourseModules.FindAsync(id);

        public async Task AddAsync(CourseModule module) =>
            await _context.CourseModules.AddAsync(module);

        public Task UpdateAsync(CourseModule module)
        {
            _context.CourseModules.Update(module);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CourseModule module)
        {
            _context.CourseModules.Remove(module);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
