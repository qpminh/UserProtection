using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public string UserId { get; set; } = null!;

    public int CourseId { get; set; }

    public DateTime EnrolledAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
