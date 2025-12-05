namespace UserProtection.Application.Dtos.Subscriptions
{
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
        public PaymentSummaryDto? Payment { get; set; }
    }

    public class PaymentSummaryDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }

    public class CreateSubscriptionRequest
    {
        public int PlanId { get; set; }
        public string? UserId { get; set; }
    }

    public class UpdateSubscriptionStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateSubscriptionDatesRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
