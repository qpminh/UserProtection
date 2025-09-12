namespace UserProtection.Domain.Entities;

public partial class SuspiciousLink
{
    public int SuspiciousId { get; set; }

    public int? TenantId { get; set; }

    public string? UserId { get; set; }

    public string Url { get; set; } = null!;

    public string? PageTitle { get; set; }

    public string? PageContent { get; set; }

    public string? HtmlContent { get; set; }

    public DateTime DetectedAt { get; set; }

    public string? CheckResult { get; set; }

    public decimal? ConfidenceScore { get; set; }

    public int? MatchedPatternId { get; set; }

    public string? ActionTaken { get; set; }

    public virtual AntiPhishingPattern? MatchedPattern { get; set; }

    public virtual Tenant? Tenant { get; set; }

    public virtual User? User { get; set; }
}
