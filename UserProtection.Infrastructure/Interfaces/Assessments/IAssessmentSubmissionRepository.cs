using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentSubmissionRepository
    {
        Task<AssessmentSubmission?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentSubmission>> GetByAssessmentAsync(int assessmentId);
        Task<IEnumerable<AssessmentSubmission>> GetByUserAsync(string userId);
        Task AddAsync(AssessmentSubmission entity);
        void Update(AssessmentSubmission entity);
        void Delete(AssessmentSubmission entity);
        Task SaveChangesAsync();
    }
}
