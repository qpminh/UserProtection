using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentSubmissionService
    {
        Task<AssessmentSubmissionDto?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentSubmissionDto>> GetByAssessmentAsync(int assessmentId);
        Task<IEnumerable<AssessmentSubmissionDto>> GetByUserAsync(string userId);
        Task<AssessmentSubmissionDto> CreateAsync(CreateAssessmentSubmissionDto dto);
        Task<AssessmentSubmissionDto> UpdateAsync(int id, UpdateAssessmentSubmissionDto dto);
        Task DeleteAsync(int id);
    }
}
