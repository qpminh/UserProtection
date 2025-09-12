namespace UserProtection.Domain.Entities;

public partial class TenantUserAccess
{
    public int AccessId { get; set; }

    public int TenantId { get; set; }

    public string UserId { get; set; } = null!;

    public int SubscriptionId { get; set; }

    public DateTime AssignedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
