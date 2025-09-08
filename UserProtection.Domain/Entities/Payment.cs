using System;
using System.Collections.Generic;

namespace UserProtection.Domain.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int SubscriptionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Subscription Subscription { get; set; } = null!;
}
