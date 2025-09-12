namespace UserProtection.Domain.Entities;

public partial class CourseModule
{
    public int ModuleId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int OrderIndex { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual Course Course { get; set; } = null!;

    public virtual User? User { get; set; }

    public virtual ICollection<UserCourseProgress> UserCourseProgresses { get; set; } = new List<UserCourseProgress>();
}
