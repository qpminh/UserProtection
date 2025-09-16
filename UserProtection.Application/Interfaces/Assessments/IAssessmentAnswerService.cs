using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentAnswerService
    {
        Task<AssessmentAnswerDto?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentAnswerDto>> GetByAttemptAsync(int attemptId);
        Task<AssessmentAnswerDto> CreateAsync(AssessmentAnswerDto dto);
        Task<AssessmentAnswerDto> UpdateAsync(int id, AssessmentAnswerDto dto);
        Task DeleteAsync(int id);
    }
}
