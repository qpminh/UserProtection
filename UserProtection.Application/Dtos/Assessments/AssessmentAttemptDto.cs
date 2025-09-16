namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentAttemptDto
{
    public int AttemptId { get; set; }
    public int AssessmentId { get; set; }
    public string UserId { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal? Score { get; set; }
    public int AttemptNumber { get; set; }
    public List<AssessmentAnswerDto> Answers { get; set; } = new();
}

public class CreateAssessmentAttemptDto
{
    public int AssessmentId { get; set; }
    public string UserId { get; set; } = null!;
    public List<CreateAssessmentAnswerDto> Answers { get; set; } = new();
}
