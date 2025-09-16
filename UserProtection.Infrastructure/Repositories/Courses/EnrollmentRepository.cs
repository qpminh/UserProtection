using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Infrastructure.Repositories.Courses
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly UserProtectionContext _context;
        public EnrollmentRepository(UserProtectionContext context) => _context = context;

        public async Task<Enrollment?> GetByIdAsync(int id) =>
            await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);

        public async Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId) =>
            await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .ToListAsync();

        public async Task<IEnumerable<Enrollment>> GetByUserAsync(string userId) =>
            await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ToListAsync();

        public async Task<Enrollment?> GetByUserAndCourseAsync(string userId, int courseId) =>
            await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);

        public async Task AddAsync(Enrollment entity) =>
            await _context.Enrollments.AddAsync(entity);

        public void Update(Enrollment entity) =>
            _context.Enrollments.Update(entity);

        public void Delete(Enrollment entity) =>
            _context.Enrollments.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
