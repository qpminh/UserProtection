namespace UserProtection.Domain.Entities;

public partial class AssessmentAnswer
{
    public int AnswerId { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int? OptionId { get; set; }

    public string? AnswerText { get; set; }

    public bool? IsCorrect { get; set; }

    public virtual AssessmentAttempt Attempt { get; set; } = null!;

    public virtual AssessmentOption? Option { get; set; }

    public virtual AssessmentQuestion Question { get; set; } = null!;
}
