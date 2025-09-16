namespace UserProtection.Application.Dtos.Subscription;

public class SubscriptionDto
{
    public int SubscriptionId { get; set; }
    public int PlanId { get; set; }
    public int? TenantId { get; set; }
    public string? UserId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool AutoRenew { get; set; }
}

public class CreateSubscriptionRequest
{
    public int PlanId { get; set; }
    public string UserId { get; set; } = null!;
}

public class UpdateSubscriptionRequest
{
    public bool? AutoRenew { get; set; }
    public string? Status { get; set; }
    public DateTime? EndDate { get; set; }
}