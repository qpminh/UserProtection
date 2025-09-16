using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Infrastructure.Repositories.Courses
{
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

        public async Task<IEnumerable<Domain.Entities.Course>> GetCoursesBySubscriptionKeyAsync(string keyValue)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Plan)
                    .ThenInclude(p => p.PlanCourses)
                        .ThenInclude(pc => pc.Course)
                .Include(s => s.SubscriptionKeys)
                .FirstOrDefaultAsync(s =>
                    s.SubscriptionKeys.Any(k => k.KeyValue == keyValue && k.IsActive) &&
                    s.Status == "Active");

            return subscription?.Plan.PlanCourses.Select(pc => pc.Course) ?? Enumerable.Empty<Domain.Entities.Course>();
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}