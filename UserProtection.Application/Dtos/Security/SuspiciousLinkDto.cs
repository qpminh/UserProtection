namespace UserProtection.Application.Dtos.Security
{
    public class SuspiciousLinkDto
    {
        public int LinkId { get; set; }
        public string Url { get; set; } = null!;
        public string? PageTitle { get; set; }
        public string? Reason { get; set; }
        public DateTime DetectedAt { get; set; }
        public string? CheckResult { get; set; }
        public decimal? ConfidenceScore { get; set; }
        public string? UserId { get; set; }

    }
}
