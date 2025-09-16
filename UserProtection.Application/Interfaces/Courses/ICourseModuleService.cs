using UserProtection.Application.Dtos.Course;

namespace UserProtection.Application.Interfaces.Courses
{
    public interface ICourseModuleService
    {
        Task<IEnumerable<CourseModuleDto>> GetByCourseAsync(int courseId);
        Task<CourseModuleDto?> GetByIdAsync(int id);
        Task<CourseModuleDto> CreateAsync(int courseId, CreateCourseModuleRequest request);
        Task UpdateAsync(int id, UpdateCourseModuleRequest request);
        Task DeleteAsync(int id);
    }
}
