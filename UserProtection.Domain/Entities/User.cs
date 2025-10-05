using Microsoft.AspNetCore.Identity;

namespace UserProtection.Domain.Entities;

public partial class User : IdentityUser
{
    public int? TenantId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<AntiPhishingPattern> AntiPhishingPatterns { get; set; } = new List<AntiPhishingPattern>();

    public virtual ICollection<AssessmentAttempt> AssessmentAttempts { get; set; } = new List<AssessmentAttempt>();

    public virtual ICollection<AssessmentQuestion> AssessmentQuestions { get; set; } = new List<AssessmentQuestion>();

    public virtual ICollection<AssessmentSubmission> AssessmentSubmissions { get; set; } = new List<AssessmentSubmission>();

    public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<CourseModule> CourseModules { get; set; } = new List<CourseModule>();

    public virtual ICollection<CourseReview> CourseReviews { get; set; } = new List<CourseReview>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<SuspiciousLink> SuspiciousLinks { get; set; } = new List<SuspiciousLink>();

    public virtual Tenant? Tenant { get; set; }

    public virtual ICollection<TenantUserAccess> TenantUserAccesses { get; set; } = new List<TenantUserAccess>();

    public virtual ICollection<TrustedLink> TrustedLinks { get; set; } = new List<TrustedLink>();

    public virtual ICollection<UserDomainEntry> UserDomainEntries { get; set; } = new List<UserDomainEntry>();

    public virtual ICollection<UserCourseProgress> UserCourseProgresses { get; set; } = new List<UserCourseProgress>();
}
