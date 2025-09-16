namespace UserProtection.Application.Dtos.Courses;

public class UserCourseProgressDto
{
    public int ProgressId { get; set; }
    public string UserId { get; set; } = null!;
    public int CourseId { get; set; }
    public int? ModuleId { get; set; }
    public decimal Progress { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime LastAccessedAt { get; set; }
}

public class CreateUserCourseProgressDto
{
    public string UserId { get; set; } = null!;
    public int CourseId { get; set; }
    public int? ModuleId { get; set; }
    public decimal Progress { get; set; }
}

public class UpdateUserCourseProgressDto
{
    public int? ModuleId { get; set; }
    public decimal Progress { get; set; }
}