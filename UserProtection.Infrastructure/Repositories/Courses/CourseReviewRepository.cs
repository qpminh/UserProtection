using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Infrastructure.Repositories.Courses
{
    public class CourseReviewRepository : ICourseReviewRepository
    {
        private readonly UserProtectionContext _context;
        public CourseReviewRepository(UserProtectionContext context) => _context = context;

        public async Task<IEnumerable<CourseReview>> GetByCourseAsync(int courseId) =>
            await _context.CourseReviews
                .Where(r => r.CourseId == courseId)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

        public async Task<CourseReview?> GetByIdAsync(int id) =>
            await _context.CourseReviews.FindAsync(id);

        public async Task AddAsync(CourseReview review) =>
            await _context.CourseReviews.AddAsync(review);

        public void Delete(CourseReview review) =>
            _context.CourseReviews.Remove(review);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
