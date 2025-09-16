namespace UserProtection.Application.Dtos.Tenant
{
    public class TenantDto
    {
        public int TenantId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Domain { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string Status { get; set; } = null!;
    }

    public class TenantCreateDto
    {
        public string CompanyName { get; set; } = null!;
        public string? Domain { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }

    public class TenantUpdateDto
    {
        public string? CompanyName { get; set; }
        public string? Domain { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
    }
}
