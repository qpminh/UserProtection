using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentQuestionService
    {
        Task<AssessmentQuestionDto?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentQuestionDto>> GetByAssessmentAsync(int assessmentId);
        Task<AssessmentQuestionDto> CreateAsync(CreateAssessmentQuestionDto dto);
        Task<AssessmentQuestionDto> UpdateAsync(int id, UpdateAssessmentQuestionDto dto);
        Task DeleteAsync(int id);
    }
}
