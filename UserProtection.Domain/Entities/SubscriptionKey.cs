namespace UserProtection.Domain.Entities;

public class SubscriptionKey
{
    public int KeyId { get; set; }
    public int SubscriptionId { get; set; }

    public string KeyValue { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public bool IsActive { get; set; }


    public virtual Subscription Subscription { get; set; } = null!;
}
