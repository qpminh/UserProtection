using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Blogs;
using UserProtection.Infrastructure.Interfaces.Core;
using UserProtection.Infrastructure.Interfaces.Courses;
using UserProtection.Infrastructure.Interfaces.Features;
using UserProtection.Infrastructure.Interfaces.Payments;
using UserProtection.Infrastructure.Interfaces.Plans;
using UserProtection.Infrastructure.Interfaces.Security;
using UserProtection.Infrastructure.Interfaces.Subscriptions;
using UserProtection.Infrastructure.Interfaces.Tenants;
using UserProtection.Infrastructure.Repositories.Blogs;
using UserProtection.Infrastructure.Repositories.Cores;
using UserProtection.Infrastructure.Repositories.Courses;
using UserProtection.Infrastructure.Repositories.Features;
using UserProtection.Infrastructure.Repositories.Payments;
using UserProtection.Infrastructure.Repositories.Plans;
using UserProtection.Infrastructure.Repositories.Security;
using UserProtection.Infrastructure.Repositories.Subscriptions;
using UserProtection.Infrastructure.Repositories.Tenants;

namespace UserProtection.Infrastructure.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UserProtectionContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Core
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            // Subscription
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionKeyRepository, SubscriptionKeyRepository>();

            // Feature
            services.AddScoped<IFeatureRepository, FeatureRepository>();

            // Plan
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanFeatureRepository, PlanFeatureRepository>();
            services.AddScoped<IPlanCourseRepository, PlanCourseRepository>();

            // Course
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseModuleRepository, CourseModuleRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IUserCourseProgressRepository, UserCourseProgressRepository>();
            services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();

            // Security
            services.AddScoped<ITrustedLinkRepository, TrustedLinkRepository>();
            services.AddScoped<ISuspiciousLinkRepository, SuspiciousLinkRepository>();
            services.AddScoped<IUserDomainEntriesRepository, UserDomainEntriesRepository>();

            // Tenant
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<ITenantUserAccessRepository, TenantUserAccessRepository>();

            services.AddScoped<IBlogRepository, BlogRepository>();

            return services;
        }
    }
}
