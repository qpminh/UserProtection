using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProtection.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "learning");

            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "billing");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "billing",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    BillingCycle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Plans__755C22B7374DD1D6", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "core",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tenants__2E9B47E1CC998FAA", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AntiPhishingPatterns",
                schema: "security",
                columns: table => new
                {
                    PatternId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatternType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PatternText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatternHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AntiPhis__0A631B52D5DCBC6A", x => x.PatternId);
                    table.ForeignKey(
                        name: "FK_APP_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_APP_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "core",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__5E5486486073394F", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AuditLogs_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "learning",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__C92D71A7944F8561", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Courses_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "billing",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    AutoRenew = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subscrip__9A2B249DF1996288", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plan",
                        column: x => x.PlanId,
                        principalSchema: "billing",
                        principalTable: "Plans",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId");
                    table.ForeignKey(
                        name: "FK_Subscriptions_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrustedLinks",
                schema: "security",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrustedL__2D1221354FF931FE", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_TL_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TL_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SuspiciousLinks",
                schema: "security",
                columns: table => new
                {
                    SuspiciousId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtmlContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    CheckResult = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MatchedPatternId = table.Column<int>(type: "int", nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Suspicio__4D9FB8445F9672B9", x => x.SuspiciousId);
                    table.ForeignKey(
                        name: "FK_SL_Pattern",
                        column: x => x.MatchedPatternId,
                        principalSchema: "security",
                        principalTable: "AntiPhishingPatterns",
                        principalColumn: "PatternId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SL_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SL_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourseModules",
                schema: "learning",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseMo__2B7477A7CCED40F0", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Modules_Course",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Modules_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CourseReviews",
                schema: "learning",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseRe__74BC79CEAA9F3D11", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Course",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Reviews_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "learning",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__7F68771B975EF4BF", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enroll_Course",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Enroll_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "billing",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Completed")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A3889A83FA2", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Subscription",
                        column: x => x.SubscriptionId,
                        principalSchema: "billing",
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId");
                });

            migrationBuilder.CreateTable(
                name: "TenantUserAccess",
                schema: "billing",
                columns: table => new
                {
                    AccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TenantUs__4130D05FEB7B0B7A", x => x.AccessId);
                    table.ForeignKey(
                        name: "FK_TUA_Sub",
                        column: x => x.SubscriptionId,
                        principalSchema: "billing",
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId");
                    table.ForeignKey(
                        name: "FK_TUA_Tenant",
                        column: x => x.TenantId,
                        principalSchema: "core",
                        principalTable: "Tenants",
                        principalColumn: "TenantId");
                    table.ForeignKey(
                        name: "FK_TUA_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                schema: "learning",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeLimitMinutes = table.Column<int>(type: "int", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__3D2BF81E83989AD6", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_Assess_Course",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Assess_Module",
                        column: x => x.ModuleId,
                        principalSchema: "learning",
                        principalTable: "CourseModules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assess_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseProgress",
                schema: "learning",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: true),
                    Progress = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAccessedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserCour__BAE29CA512BFB486", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progress_Course",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Progress_Module",
                        column: x => x.ModuleId,
                        principalSchema: "learning",
                        principalTable: "CourseModules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Progress_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAttempts",
                schema: "learning",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())"),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AttemptNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__891A68E6FEA4BA34", x => x.AttemptId);
                    table.ForeignKey(
                        name: "FK_AA_Assessment",
                        column: x => x.AssessmentId,
                        principalSchema: "learning",
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId");
                    table.ForeignKey(
                        name: "FK_AA_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentQuestions",
                schema: "learning",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__0DC06FAC52E5C7BC", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_AQ_Assessment",
                        column: x => x.AssessmentId,
                        principalSchema: "learning",
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId");
                    table.ForeignKey(
                        name: "FK_AQ_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentSubmissions",
                schema: "learning",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(sysutcdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__449EE12596F2F8ED", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_AS_Assessment",
                        column: x => x.AssessmentId,
                        principalSchema: "learning",
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId");
                    table.ForeignKey(
                        name: "FK_AS_User",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentOptions",
                schema: "learning",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__92C7A1FFB1589996", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_AO_Question",
                        column: x => x.QuestionId,
                        principalSchema: "learning",
                        principalTable: "AssessmentQuestions",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "AssessmentAnswers",
                schema: "learning",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assessme__D4825004494214D8", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Ans_Attempt",
                        column: x => x.AttemptId,
                        principalSchema: "learning",
                        principalTable: "AssessmentAttempts",
                        principalColumn: "AttemptId");
                    table.ForeignKey(
                        name: "FK_Ans_Option",
                        column: x => x.OptionId,
                        principalSchema: "learning",
                        principalTable: "AssessmentOptions",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ans_Question",
                        column: x => x.QuestionId,
                        principalSchema: "learning",
                        principalTable: "AssessmentQuestions",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntiPhishingPatterns_UserId",
                schema: "security",
                table: "AntiPhishingPatterns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_APP_Tenant_Status",
                schema: "security",
                table: "AntiPhishingPatterns",
                columns: new[] { "TenantId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Attempt",
                schema: "learning",
                table: "AssessmentAnswers",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_OptionId",
                schema: "learning",
                table: "AssessmentAnswers",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAnswers_QuestionId",
                schema: "learning",
                table: "AssessmentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AA_Assessment_User",
                schema: "learning",
                table: "AssessmentAttempts",
                columns: new[] { "AssessmentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentAttempts_UserId",
                schema: "learning",
                table: "AssessmentAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AO_Question",
                schema: "learning",
                table: "AssessmentOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentQuestions_UserId",
                schema: "learning",
                table: "AssessmentQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UX_AQ_Assessment_Order",
                schema: "learning",
                table: "AssessmentQuestions",
                columns: new[] { "AssessmentId", "OrderIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_Course_Status",
                schema: "learning",
                table: "Assessments",
                columns: new[] { "CourseId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ModuleId",
                schema: "learning",
                table: "Assessments",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_UserId",
                schema: "learning",
                table: "Assessments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AS_Assessment_User",
                schema: "learning",
                table: "AssessmentSubmissions",
                columns: new[] { "AssessmentId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSubmissions_UserId",
                schema: "learning",
                table: "AssessmentSubmissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Tenant",
                schema: "core",
                table: "AuditLogs",
                columns: new[] { "TenantId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_User",
                schema: "core",
                table: "AuditLogs",
                columns: new[] { "UserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModules_UserId",
                schema: "learning",
                table: "CourseModules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UX_Modules_Course_Order",
                schema: "learning",
                table: "CourseModules",
                columns: new[] { "CourseId", "OrderIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseReviews_CourseId",
                schema: "learning",
                table: "CourseReviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "UX_Reviews_User_Course",
                schema: "learning",
                table: "CourseReviews",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Tenant_Status",
                schema: "learning",
                table: "Courses",
                columns: new[] { "TenantId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                schema: "learning",
                table: "Courses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                schema: "learning",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "UX_Enroll_User_Course",
                schema: "learning",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Subscription",
                schema: "billing",
                table: "Payments",
                columns: new[] { "SubscriptionId", "PaymentDate" });

            migrationBuilder.CreateIndex(
                name: "UX_Payments_TransactionId",
                schema: "billing",
                table: "Payments",
                column: "TransactionId",
                unique: true,
                filter: "([TransactionId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_Name",
                schema: "billing",
                table: "Plans",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PlanId",
                schema: "billing",
                table: "Subscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Status",
                schema: "billing",
                table: "Subscriptions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Tenant",
                schema: "billing",
                table: "Subscriptions",
                column: "TenantId",
                filter: "([TenantId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_User",
                schema: "billing",
                table: "Subscriptions",
                column: "UserId",
                filter: "([UserId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousLinks_MatchedPatternId",
                schema: "security",
                table: "SuspiciousLinks",
                column: "MatchedPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousLinks_Result",
                schema: "security",
                table: "SuspiciousLinks",
                column: "CheckResult");

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousLinks_Tenant",
                schema: "security",
                table: "SuspiciousLinks",
                columns: new[] { "TenantId", "DetectedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_SuspiciousLinks_User",
                schema: "security",
                table: "SuspiciousLinks",
                columns: new[] { "UserId", "DetectedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Domain",
                schema: "core",
                table: "Tenants",
                column: "Domain",
                unique: true,
                filter: "([Domain] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUserAccess_SubscriptionId",
                schema: "billing",
                table: "TenantUserAccess",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TUA_User",
                schema: "billing",
                table: "TenantUserAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UX_TUA_Tenant_User_Sub",
                schema: "billing",
                table: "TenantUserAccess",
                columns: new[] { "TenantId", "UserId", "SubscriptionId" },
                unique: true,
                filter: "([Status]='Active')");

            migrationBuilder.CreateIndex(
                name: "IX_TrustedLinks_Domain",
                schema: "security",
                table: "TrustedLinks",
                column: "Domain");

            migrationBuilder.CreateIndex(
                name: "IX_TrustedLinks_UserId",
                schema: "security",
                table: "TrustedLinks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UX_TrustedLinks_Tenant_Domain_Url",
                schema: "security",
                table: "TrustedLinks",
                columns: new[] { "TenantId", "Domain", "Url" },
                unique: true,
                filter: "([Status]='Active')");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_User_Course",
                schema: "learning",
                table: "UserCourseProgress",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseProgress_CourseId",
                schema: "learning",
                table: "UserCourseProgress",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseProgress_ModuleId",
                schema: "learning",
                table: "UserCourseProgress",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "core",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "core",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "core",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssessmentAnswers",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "AssessmentSubmissions",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "core");

            migrationBuilder.DropTable(
                name: "CourseReviews",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "billing");

            migrationBuilder.DropTable(
                name: "SuspiciousLinks",
                schema: "security");

            migrationBuilder.DropTable(
                name: "TenantUserAccess",
                schema: "billing");

            migrationBuilder.DropTable(
                name: "TrustedLinks",
                schema: "security");

            migrationBuilder.DropTable(
                name: "UserCourseProgress",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AssessmentAttempts",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "AssessmentOptions",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "AntiPhishingPatterns",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "billing");

            migrationBuilder.DropTable(
                name: "AssessmentQuestions",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "billing");

            migrationBuilder.DropTable(
                name: "Assessments",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "CourseModules",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "core");
        }
    }
}
