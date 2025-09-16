using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Courses
{
    public interface IUserCourseProgressRepository
    {
        Task<UserCourseProgress?> GetByIdAsync(int id);
        Task<IEnumerable<UserCourseProgress>> GetByUserAsync(string userId);
        Task<UserCourseProgress?> GetByUserAndCourseAsync(string userId, int courseId);
        Task AddAsync(UserCourseProgress entity);
        Task UpdateAsync(UserCourseProgress entity);
        void Delete(UserCourseProgress entity);
        Task SaveChangesAsync();
    }
}
