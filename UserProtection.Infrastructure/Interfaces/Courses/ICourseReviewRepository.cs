using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Courses
{
    public interface ICourseReviewRepository
    {
        Task<IEnumerable<CourseReview>> GetByCourseAsync(int courseId);
        Task<CourseReview?> GetByIdAsync(int id);
        Task AddAsync(CourseReview review);
        void Delete(CourseReview review);
        Task SaveChangesAsync();
    }
}
