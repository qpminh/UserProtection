using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class CourseReview
{
    public int ReviewId { get; set; }

    public int CourseId { get; set; }

    public string UserId { get; set; } = null!;

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
