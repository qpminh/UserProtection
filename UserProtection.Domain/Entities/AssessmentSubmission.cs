using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class AssessmentSubmission
{
    public int SubmissionId { get; set; }

    public int AssessmentId { get; set; }

    public string UserId { get; set; } = null!;

    public string? Content { get; set; }

    public decimal? Grade { get; set; }

    public string? Feedback { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual Assessment Assessment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
