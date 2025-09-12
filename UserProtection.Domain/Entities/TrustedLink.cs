namespace UserProtection.Domain.Entities;

public partial class TrustedLink
{
    public int LinkId { get; set; }

    public int? TenantId { get; set; }

    public string? UserId { get; set; }

    public string Domain { get; set; } = null!;

    public string? Url { get; set; }

    public string? Category { get; set; }

    public string? Source { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Tenant? Tenant { get; set; }

    public virtual User? User { get; set; }
}
