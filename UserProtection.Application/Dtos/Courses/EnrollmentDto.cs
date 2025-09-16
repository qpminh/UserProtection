namespace UserProtection.Application.Dtos.Courses
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime EnrolledAt { get; set; }
        public string Status { get; set; } = null!;
    }

    public class CreateEnrollmentRequest
    {
        public string UserId { get; set; } = null!;
        public string Status { get; set; } = "Active"; // default
    }
}
