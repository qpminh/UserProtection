using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class UserCourseProgress
{
    public int ProgressId { get; set; }

    public string UserId { get; set; } = null!;

    public int CourseId { get; set; }

    public int? ModuleId { get; set; }

    public decimal Progress { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime LastAccessedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual CourseModule? Module { get; set; }

    public virtual User User { get; set; } = null!;
}
