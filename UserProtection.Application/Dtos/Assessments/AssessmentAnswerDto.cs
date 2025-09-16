namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentAnswerDto
{
    public int AnswerId { get; set; }
    public int AttemptId { get; set; }
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public string? AnswerText { get; set; }
    public bool? IsCorrect { get; set; }
}

public class CreateAssessmentAnswerDto
{
    public int QuestionId { get; set; }
    public int? OptionId { get; set; }
    public string? AnswerText { get; set; }
}
