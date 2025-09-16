using UserProtection.Application.Dtos.Courses;

namespace UserProtection.Application.Interfaces.Courses
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<EnrollmentDto>> GetByCourseAsync(int courseId);
        Task<IEnumerable<EnrollmentDto>> GetByUserAsync(string userId);
        Task<EnrollmentDto> CreateAsync(int courseId, CreateEnrollmentRequest request);
        Task<bool> CancelAsync(int id);
    }
}
