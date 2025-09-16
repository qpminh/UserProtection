namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentQuestionDto
{
    public int QuestionId { get; set; }
    public int AssessmentId { get; set; }
    public string QuestionText { get; set; } = null!;
    public string QuestionType { get; set; } = null!;
    public int Points { get; set; }
    public int OrderIndex { get; set; }
}

public class CreateAssessmentQuestionDto
{
    public int AssessmentId { get; set; }
    public string QuestionText { get; set; } = null!;
    public string QuestionType { get; set; } = null!;
    public int Points { get; set; }
    public int OrderIndex { get; set; }
}

public class UpdateAssessmentQuestionDto
{
    public string? QuestionText { get; set; }
    public string? QuestionType { get; set; }
    public int? Points { get; set; }
    public int? OrderIndex { get; set; }
}
