namespace UserProtection.Domain.Entities;

public partial class Assessment
{
    public int AssessmentId { get; set; }

    public int CourseId { get; set; }

    public int? ModuleId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string AssessmentType { get; set; } = null!;

    public int? TimeLimitMinutes { get; set; }

    public DateTime? DueDate { get; set; }

    public int TotalPoints { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AssessmentAttempt> AssessmentAttempts { get; set; } = new List<AssessmentAttempt>();

    public virtual ICollection<AssessmentQuestion> AssessmentQuestions { get; set; } = new List<AssessmentQuestion>();

    public virtual ICollection<AssessmentSubmission> AssessmentSubmissions { get; set; } = new List<AssessmentSubmission>();

    public virtual Course Course { get; set; } = null!;

    public virtual CourseModule? Module { get; set; }

    public virtual User? User { get; set; }
}
