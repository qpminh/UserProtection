using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class AssessmentQuestion
{
    public int QuestionId { get; set; }

    public int AssessmentId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public int Points { get; set; }

    public int OrderIndex { get; set; }

    public string? UserId { get; set; }

    public virtual Assessment Assessment { get; set; } = null!;

    public virtual ICollection<AssessmentAnswer> AssessmentAnswers { get; set; } = new List<AssessmentAnswer>();

    public virtual ICollection<AssessmentOption> AssessmentOptions { get; set; } = new List<AssessmentOption>();

    public virtual User? User { get; set; }
}
