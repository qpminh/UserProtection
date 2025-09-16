using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Infrastructure.Repositories.Courses
{
    public class UserCourseProgressRepository : IUserCourseProgressRepository
    {
        private readonly UserProtectionContext _context;
        public UserCourseProgressRepository(UserProtectionContext context) => _context = context;

        public async Task<UserCourseProgress?> GetByIdAsync(int id) =>
            await _context.UserCourseProgresses
                .Include(p => p.Course)
                .Include(p => p.User)
                .Include(p => p.Module)
                .FirstOrDefaultAsync(p => p.ProgressId == id);

        public async Task<IEnumerable<UserCourseProgress>> GetByUserAsync(string userId) =>
            await _context.UserCourseProgresses
                .Where(p => p.UserId == userId)
                .Include(p => p.Course)
                .Include(p => p.Module)
                .ToListAsync();

        public async Task<UserCourseProgress?> GetByUserAndCourseAsync(string userId, int courseId) =>
            await _context.UserCourseProgresses
                .Include(p => p.Course)
                .Include(p => p.Module)
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);

        public async Task AddAsync(UserCourseProgress entity) =>
            await _context.UserCourseProgresses.AddAsync(entity);

        public Task UpdateAsync(UserCourseProgress entity)
        {
            _context.UserCourseProgresses.Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(UserCourseProgress entity) =>
            _context.UserCourseProgresses.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
