using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentQuestionRepository
    {
        Task<AssessmentQuestion?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentQuestion>> GetByAssessmentAsync(int assessmentId);
        Task AddAsync(AssessmentQuestion entity);
        void Update(AssessmentQuestion entity);
        void Delete(AssessmentQuestion entity);
        Task SaveChangesAsync();
    }
}
