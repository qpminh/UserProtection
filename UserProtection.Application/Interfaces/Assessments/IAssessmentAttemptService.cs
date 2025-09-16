using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentAttemptService
    {
        Task<AssessmentAttemptDto> StartAttemptAsync(CreateAssessmentAttemptDto dto);
        Task<AssessmentAttemptDto?> GetAttemptAsync(int id);
    }
}
