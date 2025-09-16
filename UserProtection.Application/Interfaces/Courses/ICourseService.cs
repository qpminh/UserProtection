using UserProtection.Application.Dtos.Courses;

namespace UserProtection.Application.Interfaces.Courses
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task<CourseDto> CreateAsync(CreateCourseRequest request);
        Task UpdateAsync(int id, UpdateCourseRequest request);
        Task DeleteAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByKeyAsync(string apiKey);
    }
}
