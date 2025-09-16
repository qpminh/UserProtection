namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentDto
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
    public string Status { get; set; } = null!;
}

public class CreateAssessmentDto
{
    public int CourseId { get; set; }
    public int? ModuleId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string AssessmentType { get; set; } = null!;
    public int? TimeLimitMinutes { get; set; }
    public DateTime? DueDate { get; set; }
    public int TotalPoints { get; set; }
}

public class UpdateAssessmentDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AssessmentType { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public DateTime? DueDate { get; set; }
    public int? TotalPoints { get; set; }
    public string? Status { get; set; }
}
