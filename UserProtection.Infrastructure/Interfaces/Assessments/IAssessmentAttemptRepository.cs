using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentAttemptRepository
    {
        Task<AssessmentAttempt?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentAttempt>> GetByAssessmentAsync(int assessmentId, string userId);
        Task<int> GetNextAttemptNumberAsync(int assessmentId, string userId);
        Task AddAsync(AssessmentAttempt entity);
        void Update(AssessmentAttempt entity);
        Task SaveChangesAsync();
    }
}
