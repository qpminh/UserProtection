using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class AssessmentOption
{
    public int OptionId { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual ICollection<AssessmentAnswer> AssessmentAnswers { get; set; } = new List<AssessmentAnswer>();

    public virtual AssessmentQuestion Question { get; set; } = null!;
}
