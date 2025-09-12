namespace UserProtection.Domain.Entities;

public partial class Course
{
    public int CourseId { get; set; }

    public int? TenantId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Level { get; set; }

    public decimal Price { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

    public virtual ICollection<CourseReview> CourseReviews { get; set; } = new List<CourseReview>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Tenant? Tenant { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<UserCourseProgress> UserCourseProgresses { get; set; } = new List<UserCourseProgress>();
    public virtual ICollection<PlanCourse> PlanCourses { get; set; } = new List<PlanCourse>();
}
