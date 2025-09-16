using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentRepository
    {
        Task<Assessment?> GetByIdAsync(int id);
        Task<IEnumerable<Assessment>> GetByCourseAsync(int courseId);
        Task AddAsync(Assessment entity);
        void Update(Assessment entity);
        void Delete(Assessment entity);
        Task SaveChangesAsync();
    }
}
