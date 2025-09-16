using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentAnswerRepository
    {
        Task<AssessmentAnswer?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentAnswer>> GetByAttemptAsync(int attemptId);
        Task AddAsync(AssessmentAnswer entity);
        Task AddRangeAsync(IEnumerable<AssessmentAnswer> entities);
        void Update(AssessmentAnswer entity);
        void Delete(AssessmentAnswer entity);
        Task SaveChangesAsync();
    }
}
