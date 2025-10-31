using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.SeedData
{
    public static class SecuritySeeder
    {
        public static async Task SeedAsync(UserProtectionContext context)
        {
            await context.Database.MigrateAsync();

            // ================= SEED TRUSTED LINKS =================
            if (!await context.TrustedLinks.AnyAsync())
            {
                var trustedLinks = new List<TrustedLink>
                {
                    new TrustedLink { TenantId = null, UserId = null, Domain = "facebook.com", Url = "https://www.facebook.com", Category = "Social Media", Source = "Facebook", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "google.com", Url = "https://www.google.com", Category = "Search Engine", Source = "Google", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "twitter.com", Url = "https://www.twitter.com", Category = "Social Media", Source = "Twitter", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "instagram.com", Url = "https://www.instagram.com", Category = "Social Media", Source = "Instagram", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "linkedin.com", Url = "https://www.linkedin.com", Category = "Professional Network", Source = "LinkedIn", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "youtube.com", Url = "https://www.youtube.com", Category = "Video Sharing", Source = "YouTube", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "github.com", Url = "https://www.github.com", Category = "Development", Source = "GitHub", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "reddit.com", Url = "https://www.reddit.com", Category = "Discussion Forum", Source = "Reddit", Status = "Active" },
                    new TrustedLink { TenantId = null, UserId = null, Domain = "pinterest.com", Url = "https://www.pinterest.com", Category = "Social Media", Source = "Pinterest", Status = "Active" }
                };

                await context.TrustedLinks.AddRangeAsync(trustedLinks);
                await context.SaveChangesAsync();
            }

            // ================= SEED SUSPICIOUS LINKS =================
            if (!await context.SuspiciousLinks.AnyAsync())
            {
                var suspiciousLinks = new List<SuspiciousLink>
                {
                    new SuspiciousLink { UserId = null, Url = "http://example.com/malicious-link", PageTitle = "Suspicious Link Detected", PageContent = "This page contains suspicious content related to phishing.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:00:00Z"), CheckResult = "Malicious", ConfidenceScore = 98.5M, ActionTaken = "Quarantined", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/phishing-page", PageTitle = "Phishing Attempt", PageContent = "The page is designed to steal personal information.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:05:00Z"), CheckResult = "Phishing", ConfidenceScore = 95.0M, ActionTaken = "Removed", Status = "Inactive" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/suspicious-link2", PageTitle = "Suspicious URL", PageContent = "The page contains an embedded malicious script.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:10:00Z"), CheckResult = "Malicious Script", ConfidenceScore = 97.0M, ActionTaken = "Blocked", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/malware-download", PageTitle = "Malware Download", PageContent = "This page tries to download harmful files.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:15:00Z"), CheckResult = "Malware", ConfidenceScore = 99.0M, ActionTaken = "Quarantined", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/fake-login", PageTitle = "Fake Login Page", PageContent = "The page mimics a genuine login form to capture credentials.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:20:00Z"), CheckResult = "Credential Harvesting", ConfidenceScore = 96.0M, ActionTaken = "Removed", Status = "Inactive" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/untrusted-link", PageTitle = "Untrusted Link", PageContent = "The link redirects to an untrusted external site.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:25:00Z"), CheckResult = "Untrusted Redirect", ConfidenceScore = 91.0M, ActionTaken = "Blocked", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/malicious-ad", PageTitle = "Malicious Advertisement", PageContent = "This page contains a malicious advertisement that attempts to install malware.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:30:00Z"), CheckResult = "Malicious Ad", ConfidenceScore = 94.5M, ActionTaken = "Quarantined", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/scam", PageTitle = "Scam Alert", PageContent = "This site is identified as part of a scam campaign.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:35:00Z"), CheckResult = "Scam", ConfidenceScore = 92.0M, ActionTaken = "Removed", Status = "Inactive" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/redirect", PageTitle = "Suspicious Redirect", PageContent = "The page contains an automatic redirect to a suspicious site.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:40:00Z"), CheckResult = "Redirect", ConfidenceScore = 93.0M, ActionTaken = "Blocked", Status = "Active" },
                    new SuspiciousLink { UserId = null, Url = "http://example.com/drive-by-download", PageTitle = "Drive-By Download Attempt", PageContent = "This page attempts to download malicious software without user consent.", HtmlContent = "<html>...</html>", DetectedAt = DateTime.Parse("2025-09-30T12:45:00Z"), CheckResult = "Drive-By Download", ConfidenceScore = 99.5M, ActionTaken = "Quarantined", Status = "Active" }
                };

                await context.SuspiciousLinks.AddRangeAsync(suspiciousLinks);
                await context.SaveChangesAsync();
            }
        }
    }
}
