using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentOptionService
    {
        Task<AssessmentOptionDto?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentOptionDto>> GetByQuestionAsync(int questionId);
        Task<AssessmentOptionDto> CreateAsync(CreateAssessmentOptionDto dto);
        Task<AssessmentOptionDto> UpdateAsync(int id, UpdateAssessmentOptionDto dto);
        Task DeleteAsync(int id);
    }
}
