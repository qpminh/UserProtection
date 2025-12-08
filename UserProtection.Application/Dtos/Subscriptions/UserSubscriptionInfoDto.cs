namespace UserProtection.Application.Dtos.Subscriptions
{
    public class UserSubscriptionInfoDto
    {
        public string UserId { get; set; } = null!;
        public List<UserSubscriptionDetailDto> Subscriptions { get; set; } = new();
    }

    public class UserSubscriptionDetailDto
    {
        public int SubscriptionId { get; set; }
        public string PlanName { get; set; } = null!;
        public string? PlanDescription { get; set; }
        public string Status { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> Features { get; set; } = new();
        public List<UserPaymentDto> Payments { get; set; } = new();
    }

    public class UserPaymentDto
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        //public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }
}
