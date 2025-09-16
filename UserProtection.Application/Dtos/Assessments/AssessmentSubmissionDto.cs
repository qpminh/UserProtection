namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentSubmissionDto
{
    public int SubmissionId { get; set; }
    public int AssessmentId { get; set; }
    public string UserId { get; set; } = null!;
    public string? Content { get; set; }
    public decimal? Grade { get; set; }
    public string? Feedback { get; set; }
    public DateTime SubmittedAt { get; set; }
}

public class CreateAssessmentSubmissionDto
{
    public int AssessmentId { get; set; }
    public string UserId { get; set; } = null!;
    public string? Content { get; set; }
}

public class UpdateAssessmentSubmissionDto
{
    public decimal? Grade { get; set; }
    public string? Feedback { get; set; }
}
