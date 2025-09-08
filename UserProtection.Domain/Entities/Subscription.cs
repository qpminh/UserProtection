using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int? TenantId { get; set; }

    public string? UserId { get; set; }

    public int PlanId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public bool AutoRenew { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Plan Plan { get; set; } = null!;

    public virtual Tenant? Tenant { get; set; }

    public virtual ICollection<TenantUserAccess> TenantUserAccesses { get; set; } = new List<TenantUserAccess>();

    public virtual User? User { get; set; }
}
