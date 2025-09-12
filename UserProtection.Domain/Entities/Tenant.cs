namespace UserProtection.Domain.Entities;

public partial class Tenant
{
    public int TenantId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Domain { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AntiPhishingPattern> AntiPhishingPatterns { get; set; } = new List<AntiPhishingPattern>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<SuspiciousLink> SuspiciousLinks { get; set; } = new List<SuspiciousLink>();

    public virtual ICollection<TenantUserAccess> TenantUserAccesses { get; set; } = new List<TenantUserAccess>();

    public virtual ICollection<TrustedLink> TrustedLinks { get; set; } = new List<TrustedLink>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
