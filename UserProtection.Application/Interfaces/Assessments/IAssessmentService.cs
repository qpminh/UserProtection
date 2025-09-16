using UserProtection.Application.Dtos.Assessments;

namespace UserProtection.Application.Interfaces.Assessments
{
    public interface IAssessmentService
    {
        Task<AssessmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<AssessmentDto>> GetByCourseAsync(int courseId);
        Task<AssessmentDto> CreateAsync(CreateAssessmentDto dto);
        Task<AssessmentDto> UpdateAsync(int id, UpdateAssessmentDto dto);
        Task DeleteAsync(int id);
    }
}
