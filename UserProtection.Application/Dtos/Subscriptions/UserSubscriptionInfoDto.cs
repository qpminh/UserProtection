namespace UserProtection.Application.Dtos.Subscriptions
{
    public class UserSubscriptionInfoDto
    {
        public string? UserId { get; set; }
        public string? PlanName { get; set; }
        public string? PlanDescription { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<string> Features { get; set; } = new List<string>();
    }
}
