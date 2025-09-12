namespace UserProtection.Domain.Entities;

public partial class AuditLog
{
    public int LogId { get; set; }

    public int? TenantId { get; set; }

    public string? UserId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Metadata { get; set; }

    public virtual Tenant? Tenant { get; set; }

    public virtual User? User { get; set; }
}
