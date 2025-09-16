using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Courses
{
    public interface ICourseModuleRepository
    {
        Task<IEnumerable<CourseModule>> GetByCourseAsync(int courseId);
        Task<CourseModule?> GetByIdAsync(int id);
        Task AddAsync(CourseModule module);
        Task UpdateAsync(CourseModule module);
        Task DeleteAsync(CourseModule module);
        Task SaveChangesAsync();
    }
}
