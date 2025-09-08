using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string BillingCycle { get; set; } = null!;

    public string? Features { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
