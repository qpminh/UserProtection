using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.Interfaces.Assessments
{
    public interface IAssessmentOptionRepository
    {
        Task<AssessmentOption?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentOption>> GetByQuestionAsync(int questionId);
        Task AddAsync(AssessmentOption entity);
        void Update(AssessmentOption entity);
        void Delete(AssessmentOption entity);
        Task SaveChangesAsync();
    }
}
