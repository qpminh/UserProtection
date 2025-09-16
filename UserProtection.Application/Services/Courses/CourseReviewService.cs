using AutoMapper;
using UserProtection.Application.Dtos.Courses;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Courses;

namespace UserProtection.Application.Services.Courses
{
    public class CourseReviewService : ICourseReviewService
    {
        private readonly ICourseReviewRepository _repo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IMapper _mapper;

        public CourseReviewService(
            ICourseReviewRepository repo,
            IEnrollmentRepository enrollmentRepo,
            IMapper mapper)
        {
            _repo = repo;
            _enrollmentRepo = enrollmentRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseReviewDto>> GetByCourseAsync(int courseId) =>
            _mapper.Map<IEnumerable<CourseReviewDto>>(await _repo.GetByCourseAsync(courseId));

        public async Task<CourseReviewDto> CreateOrUpdateAsync(int courseId, CreateCourseReviewRequest request)
        {
            // Rule: user phải enroll trước khi review
            var enrollments = await _enrollmentRepo.GetByUserAsync(request.UserId);
            var hasEnrollment = enrollments.Any(e => e.CourseId == courseId && e.Status == "Active");
            if (!hasEnrollment)
                throw new InvalidOperationException("User must enroll in the course before reviewing.");

            // Check xem user đã review chưa
            var existingReviews = await _repo.GetByCourseAsync(courseId);
            var userReview = existingReviews.FirstOrDefault(r => r.UserId == request.UserId);

            if (userReview != null)
            {
                // Update review cũ
                userReview.Rating = request.Rating;
                userReview.Comment = request.Comment;
                userReview.CreatedAt = DateTime.UtcNow;

                await _repo.SaveChangesAsync();
                return _mapper.Map<CourseReviewDto>(userReview);
            }

            // Tạo review mới
            var review = new CourseReview
            {
                CourseId = courseId,
                UserId = request.UserId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(review);
            await _repo.SaveChangesAsync();

            return _mapper.Map<CourseReviewDto>(review);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var r = await _repo.GetByIdAsync(id);
            if (r == null) return false;
            _repo.Delete(r);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
