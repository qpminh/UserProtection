using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserProtection.Domain.Entities;

public partial class UserProtectionContext : IdentityDbContext<User>
{
    public UserProtectionContext()
    {
    }

    public UserProtectionContext(DbContextOptions<UserProtectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AntiPhishingPattern> AntiPhishingPatterns { get; set; }

    public virtual DbSet<Assessment> Assessments { get; set; }

    public virtual DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }

    public virtual DbSet<AssessmentAttempt> AssessmentAttempts { get; set; }

    public virtual DbSet<AssessmentOption> AssessmentOptions { get; set; }

    public virtual DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }

    public virtual DbSet<AssessmentSubmission> AssessmentSubmissions { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseModule> CourseModules { get; set; }

    public virtual DbSet<CourseReview> CourseReviews { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SubscriptionKey> SubscriptionKeys { get; set; }

    public virtual DbSet<SuspiciousLink> SuspiciousLinks { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantUserAccess> TenantUserAccesses { get; set; }

    public virtual DbSet<TrustedLink> TrustedLinks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourseProgress> UserCourseProgresses { get; set; }

    public virtual DbSet<UserDomainEntry> UserDomainEntries { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<PlanFeature> PlanFeatures { get; set; }

    public virtual DbSet<PlanCourse> PlanCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=UserProtection;Uid=sa;Pwd=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AntiPhishingPattern>(entity =>
        {
            entity.HasKey(e => e.PatternId).HasName("PK__AntiPhis__0A631B52D5DCBC6A");

            entity.ToTable("AntiPhishingPatterns", "security");

            entity.HasIndex(e => new { e.TenantId, e.Status }, "IX_APP_Tenant_Status");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PatternType).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AntiPhishingPatterns)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_APP_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.AntiPhishingPatterns)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_APP_User");
        });

        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.HasKey(e => e.AssessmentId).HasName("PK__Assessme__3D2BF81E83989AD6");

            entity.ToTable("Assessments", "learning");

            entity.HasIndex(e => new { e.CourseId, e.Status }, "IX_Assessments_Course_Status");

            entity.Property(e => e.AssessmentType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Course).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assess_Course");

            entity.HasOne(d => d.Module).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Assess_Module");

            entity.HasOne(d => d.User).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Assess_User");
        });

        modelBuilder.Entity<AssessmentAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Assessme__D4825004494214D8");

            entity.ToTable("AssessmentAnswers", "learning");

            entity.HasIndex(e => e.AttemptId, "IX_Answers_Attempt");

            entity.HasOne(d => d.Attempt).WithMany(p => p.AssessmentAnswers)
                .HasForeignKey(d => d.AttemptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ans_Attempt");

            entity.HasOne(d => d.Option).WithMany(p => p.AssessmentAnswers)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Ans_Option");

            entity.HasOne(d => d.Question).WithMany(p => p.AssessmentAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ans_Question");
        });

        modelBuilder.Entity<AssessmentAttempt>(entity =>
        {
            entity.HasKey(e => e.AttemptId).HasName("PK__Assessme__891A68E6FEA4BA34");

            entity.ToTable("AssessmentAttempts", "learning");

            entity.HasIndex(e => new { e.AssessmentId, e.UserId }, "IX_AA_Assessment_User");

            entity.Property(e => e.AttemptNumber).HasDefaultValue(1);
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StartedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Assessment).WithMany(p => p.AssessmentAttempts)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AA_Assessment");

            entity.HasOne(d => d.User).WithMany(p => p.AssessmentAttempts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AA_User");
        });

        modelBuilder.Entity<AssessmentOption>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Assessme__92C7A1FFB1589996");

            entity.ToTable("AssessmentOptions", "learning");

            entity.HasIndex(e => e.QuestionId, "IX_AO_Question");

            entity.HasOne(d => d.Question).WithMany(p => p.AssessmentOptions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AO_Question");
        });

        modelBuilder.Entity<AssessmentQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Assessme__0DC06FAC52E5C7BC");

            entity.ToTable("AssessmentQuestions", "learning");

            entity.HasIndex(e => new { e.AssessmentId, e.OrderIndex }, "UX_AQ_Assessment_Order").IsUnique();

            entity.Property(e => e.Points).HasDefaultValue(1);
            entity.Property(e => e.QuestionType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Assessment).WithMany(p => p.AssessmentQuestions)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AQ_Assessment");

            entity.HasOne(d => d.User).WithMany(p => p.AssessmentQuestions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_AQ_User");
        });

        modelBuilder.Entity<AssessmentSubmission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Assessme__449EE12596F2F8ED");

            entity.ToTable("AssessmentSubmissions", "learning");

            entity.HasIndex(e => new { e.AssessmentId, e.UserId }, "IX_AS_Assessment_User");

            entity.Property(e => e.Grade).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Assessment).WithMany(p => p.AssessmentSubmissions)
                .HasForeignKey(d => d.AssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AS_Assessment");

            entity.HasOne(d => d.User).WithMany(p => p.AssessmentSubmissions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AS_User");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5486486073394F");

            entity.ToTable("AuditLogs", "core");

            entity.HasIndex(e => new { e.TenantId, e.CreatedAt }, "IX_AuditLogs_Tenant");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "IX_AuditLogs_User");

            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Tenant).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_AuditLogs_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_AuditLogs_User");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A7944F8561");

            entity.ToTable("Courses", "learning");

            entity.HasIndex(e => new { e.TenantId, e.Status }, "IX_Courses_Tenant_Status");

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Tenant).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Courses_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.Courses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Courses_User");
        });

        modelBuilder.Entity<PlanCourse>(entity =>
        {
            entity.ToTable("PlanCourses", "billing");

            // composite key
            entity.HasKey(pc => new { pc.PlanId, pc.CourseId });

            entity.HasOne(pc => pc.Plan)
                  .WithMany(p => p.PlanCourses)
                  .HasForeignKey(pc => pc.PlanId);

            entity.HasOne(pc => pc.Course)
                  .WithMany(c => c.PlanCourses)
                  .HasForeignKey(pc => pc.CourseId);
        });

        modelBuilder.Entity<PlanFeature>(entity =>
        {
            entity.ToTable("PlanFeatures", "billing");

            entity.HasKey(pf => new { pf.PlanId, pf.FeatureId });

            entity.HasOne(pf => pf.Plan)
                  .WithMany(p => p.PlanFeatures)
                  .HasForeignKey(pf => pf.PlanId);

            entity.HasOne(pf => pf.Feature)
                  .WithMany(f => f.PlanFeatures)
                  .HasForeignKey(pf => pf.FeatureId);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.ToTable("Features", "billing");

            entity.HasKey(f => f.FeatureId);

            entity.Property(f => f.Name).HasMaxLength(100);
            entity.Property(f => f.Description).HasMaxLength(500);
        });

        modelBuilder.Entity<CourseModule>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__CourseMo__2B7477A7CCED40F0");

            entity.ToTable("CourseModules", "learning");

            entity.HasIndex(e => new { e.CourseId, e.OrderIndex }, "UX_Modules_Course_Order").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseModules)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modules_Course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseModules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Modules_User");
        });

        modelBuilder.Entity<CourseReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__CourseRe__74BC79CEAA9F3D11");

            entity.ToTable("CourseReviews", "learning");

            entity.HasIndex(e => new { e.UserId, e.CourseId }, "UX_Reviews_User_Course").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseReviews)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseReviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_User");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F68771B975EF4BF");

            entity.ToTable("Enrollments", "learning");

            entity.HasIndex(e => new { e.UserId, e.CourseId }, "UX_Enroll_User_Course").IsUnique();

            entity.Property(e => e.EnrolledAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_Course");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enroll_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3889A83FA2");

            entity.ToTable("Payments", "billing");

            entity.HasIndex(e => new { e.SubscriptionId, e.PaymentDate }, "IX_Payments_Subscription");

            entity.HasIndex(e => e.TransactionId, "UX_Payments_TransactionId")
                .IsUnique()
                .HasFilter("([TransactionId] IS NOT NULL)");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Completed");
            entity.Property(e => e.TransactionId).HasMaxLength(255);

            entity.Property(e => e.FrontendReturnUrl)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Subscription).WithMany(p => p.Payments)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Subscription");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plans__755C22B7374DD1D6");

            entity.ToTable("Plans", "billing");

            entity.HasIndex(e => e.Name, "IX_Plans_Name").IsUnique();

            entity.Property(e => e.BillingCycle).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(450);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B249DF1996288");

            entity.ToTable("Subscriptions", "billing");

            entity.HasIndex(e => e.Status, "IX_Subscriptions_Status");

            entity.HasIndex(e => e.TenantId, "IX_Subscriptions_Tenant").HasFilter("([TenantId] IS NOT NULL)");

            entity.HasIndex(e => e.UserId, "IX_Subscriptions_User").HasFilter("([UserId] IS NOT NULL)");

            entity.Property(e => e.AutoRenew).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Plan).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subscriptions_Plan");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.TenantId)
                .HasConstraintName("FK_Subscriptions_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Subscriptions_User");
        });

        modelBuilder.Entity<SubscriptionKey>(entity =>
        {
            entity.HasKey(e => e.KeyId).HasName("PK_SubscriptionKeys");

            entity.ToTable("SubscriptionKeys", "billing");

            entity.HasIndex(e => e.KeyValue).IsUnique();

            entity.Property(e => e.KeyValue)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.HasOne(d => d.Subscription)
                .WithMany(p => p.SubscriptionKeys)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SubscriptionKeys_Subscriptions");
        });

        modelBuilder.Entity<SuspiciousLink>(entity =>
        {
            entity.HasKey(e => e.SuspiciousId).HasName("PK__Suspicio__4D9FB8445F9672B9");

            entity.ToTable("SuspiciousLinks", "security");

            entity.HasIndex(e => e.CheckResult, "IX_SuspiciousLinks_Result");

            entity.HasIndex(e => new { e.TenantId, e.DetectedAt }, "IX_SuspiciousLinks_Tenant");

            entity.HasIndex(e => new { e.UserId, e.DetectedAt }, "IX_SuspiciousLinks_User");

            entity.Property(e => e.ActionTaken).HasMaxLength(50);
            entity.Property(e => e.CheckResult).HasMaxLength(50);
            entity.Property(e => e.ConfidenceScore).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.DetectedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PageTitle).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.MatchedPattern).WithMany(p => p.SuspiciousLinks)
                .HasForeignKey(d => d.MatchedPatternId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SL_Pattern");

            entity.HasOne(d => d.Tenant).WithMany(p => p.SuspiciousLinks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SL_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.SuspiciousLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SL_User");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.TenantId).HasName("PK__Tenants__2E9B47E1CC998FAA");

            entity.ToTable("Tenants", "core");

            entity.HasIndex(e => e.Domain, "IX_Tenants_Domain")
                .IsUnique()
                .HasFilter("([Domain] IS NOT NULL)");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Domain).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
        });

        modelBuilder.Entity<TenantUserAccess>(entity =>
        {
            entity.HasKey(e => e.AccessId).HasName("PK__TenantUs__4130D05FEB7B0B7A");

            entity.ToTable("TenantUserAccess", "billing");

            entity.HasIndex(e => e.UserId, "IX_TUA_User");

            entity.HasIndex(e => new { e.TenantId, e.UserId, e.SubscriptionId }, "UX_TUA_Tenant_User_Sub")
                .IsUnique()
                .HasFilter("([Status]='Active')");

            entity.Property(e => e.AssignedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Subscription).WithMany(p => p.TenantUserAccesses)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUA_Sub");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantUserAccesses)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUA_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.TenantUserAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TUA_User");
        });

        modelBuilder.Entity<TrustedLink>(entity =>
        {
            entity.HasKey(e => e.LinkId).HasName("PK__TrustedL__2D1221354FF931FE");

            entity.ToTable("TrustedLinks", "security");

            entity.HasIndex(e => e.Domain, "IX_TrustedLinks_Domain");

            entity.HasIndex(e => new { e.TenantId, e.Domain, e.Url }, "UX_TrustedLinks_Tenant_Domain_Url")
                .IsUnique()
                .HasFilter("([Status]='Active')");

            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Domain).HasMaxLength(255);
            entity.Property(e => e.Source).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Tenant).WithMany(p => p.TrustedLinks)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TL_Tenant");

            entity.HasOne(d => d.User).WithMany(p => p.TrustedLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TL_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "core");

            entity.HasIndex(e => e.TenantId, "IX_Users_TenantId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.Tenant)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.TenantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Tenants");
        });

        modelBuilder.Entity<UserCourseProgress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__UserCour__BAE29CA512BFB486");

            entity.ToTable("UserCourseProgress", "learning");

            entity.HasIndex(e => new { e.UserId, e.CourseId }, "IX_Progress_User_Course");

            entity.Property(e => e.LastAccessedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Progress).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.UserCourseProgresses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Progress_Course");

            entity.HasOne(d => d.Module).WithMany(p => p.UserCourseProgresses)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Progress_Module");

            entity.HasOne(d => d.User).WithMany(p => p.UserCourseProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Progress_User");
        });

        modelBuilder.Entity<UserDomainEntry>(entity =>
        {
            entity.ToTable("UserDomainEntries", "security");

            entity.HasKey(e => e.EntryId);

            entity.Property(e => e.EntryId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsRequired();

            entity.Property(e => e.Domain)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.EntryType)
                .IsRequired();

            entity.Property(e => e.Safe);

            entity.Property(e => e.Status)
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(e => e.User)
                .WithMany(u => u.UserDomainEntries)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.UserId, e.Domain, e.EntryType })
                .IsUnique()
                .HasDatabaseName("UX_UDE_User_Domain_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
