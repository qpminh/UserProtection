using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class AssessmentAttempt
{
    public int AttemptId { get; set; }

    public int AssessmentId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public decimal? Score { get; set; }

    public int AttemptNumber { get; set; }

    public virtual Assessment Assessment { get; set; } = null!;

    public virtual ICollection<AssessmentAnswer> AssessmentAnswers { get; set; } = new List<AssessmentAnswer>();

    public virtual User User { get; set; } = null!;
}
