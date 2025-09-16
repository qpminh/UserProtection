namespace UserProtection.Application.Dtos.Course
{
    public class CourseReviewDto
    {
        public int ReviewId { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCourseReviewRequest
    {
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
