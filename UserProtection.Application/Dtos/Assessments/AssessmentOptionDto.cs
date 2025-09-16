namespace UserProtection.Application.Dtos.Assessments;

public class AssessmentOptionDto
{
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; } = null!;
    public bool IsCorrect { get; set; }
}

public class CreateAssessmentOptionDto
{
    public int QuestionId { get; set; }
    public string OptionText { get; set; } = null!;
    public bool IsCorrect { get; set; }
}

public class UpdateAssessmentOptionDto
{
    public string? OptionText { get; set; }
    public bool? IsCorrect { get; set; }
}
