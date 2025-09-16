namespace UserProtection.Application.Dtos.Subscriptions;

public class SubscriptionKeyDto
{
    public string KeyValue { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

