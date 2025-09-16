using UserProtection.Application.Dtos.Courses;

namespace UserProtection.Application.Interfaces.Courses
{
    public interface ICourseReviewService
    {
        Task<IEnumerable<CourseReviewDto>> GetByCourseAsync(int courseId);
        Task<CourseReviewDto> CreateOrUpdateAsync(int courseId, CreateCourseReviewRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
