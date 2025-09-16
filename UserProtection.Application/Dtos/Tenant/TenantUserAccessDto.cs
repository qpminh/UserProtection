namespace UserProtection.Application.Dtos.Tenant
{
    public class TenantUserAccessDto
    {
        public int AccessId { get; set; }
        public int TenantId { get; set; }
        public string UserId { get; set; } = null!;
        public int SubscriptionId { get; set; }
        public DateTime AssignedAt { get; set; }
        public string Status { get; set; } = null!;
    }

    public class TenantUserAccessCreateDto
    {
        public int TenantId { get; set; }
        public string UserId { get; set; } = null!;
        public int SubscriptionId { get; set; }
        public string Status { get; set; } = "Active";
    }

    public class TenantUserAccessUpdateDto
    {
        public string? Status { get; set; }
    }
}
