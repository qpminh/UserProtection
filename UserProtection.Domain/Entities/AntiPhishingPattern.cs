using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class AntiPhishingPattern
{
    public int PatternId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? PatternType { get; set; }

    public string? PatternText { get; set; }

    public string? PatternHtml { get; set; }

    public string? Source { get; set; }

    public string? UserId { get; set; }

    public int? TenantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<SuspiciousLink> SuspiciousLinks { get; set; } = new List<SuspiciousLink>();

    public virtual Tenant? Tenant { get; set; }

    public virtual User? User { get; set; }
}
