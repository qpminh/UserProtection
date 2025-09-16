using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Courses
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId);
        Task<IEnumerable<Enrollment>> GetByUserAsync(string userId);
        Task<Enrollment?> GetByUserAndCourseAsync(string userId, int courseId);
        Task AddAsync(Enrollment entity);
        void Update(Enrollment entity);
        void Delete(Enrollment entity);
        Task SaveChangesAsync();
    }
}
