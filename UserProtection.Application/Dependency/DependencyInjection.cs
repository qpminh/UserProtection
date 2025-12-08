using Microsoft.Extensions.DependencyInjection;
using UserProtection.Application.Interfaces.Blogs;
using UserProtection.Application.Interfaces.Cores;
using UserProtection.Application.Interfaces.Courses;
using UserProtection.Application.Interfaces.Features;
using UserProtection.Application.Interfaces.Gemini;
using UserProtection.Application.Interfaces.Payments;
using UserProtection.Application.Interfaces.Plans;
using UserProtection.Application.Interfaces.Security;
using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Application.Interfaces.Tenants;
using UserProtection.Application.Mappers;
using UserProtection.Application.Services.Blogs;
using UserProtection.Application.Services.Cores;
using UserProtection.Application.Services.Courses;
using UserProtection.Application.Services.Features;
using UserProtection.Application.Services.Gemini;
using UserProtection.Application.Services.Payments;
using UserProtection.Application.Services.Plans;
using UserProtection.Application.Services.Security;
using UserProtection.Application.Services.Subscriptions;
using UserProtection.Application.Services.Tenants;

namespace UserProtection.Application.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //    services.AddMediatR(cfg =>
            //        cfg.RegisterServicesFromAssemblies(
            //    typeof(UserProtection.Application.Dependency.DependencyInjection).Assembly, // Correctly registers the application layer
            //    typeof(UserProtection.Application.Features.Products.Queries.UsersById).Assembly // Add this line
            //));

            services.AddAutoMapper(typeof(MapProfile).Assembly);

            // Core
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAppAdminConfigService, AppAdminConfigService>();

            // Subscription
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionKeyService, SubscriptionKeyService>();

            // Payment
            services.AddScoped<IPaymentService, PaymentService>();

            // Feature
            services.AddScoped<IFeatureService, FeatureService>();

            // Plan
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IPlanFeatureService, PlanFeatureService>();
            services.AddScoped<IPlanCourseService, PlanCourseService>();

            // Course
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseReviewService, CourseReviewService>();
            services.AddScoped<ICourseModuleService, CourseModuleService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();

            // Security
            services.AddScoped<ITrustedLinkService, TrustedLinkService>();
            services.AddScoped<ISuspiciousLinkService, SuspiciousLinkService>();
            services.AddScoped<IAntiPhishingService, AntiPhishingService>();
            services.AddScoped<IUserDomainEntriesService, UserDomainEntriesService>();

            // Tenant
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ITenantUserAccessService, TenantUserAccessService>();

            //Gemini
            services.AddScoped<IAIService, GeminiAIService>();

            services.AddScoped<IBlogService, BlogService>();

            return services;
        }
    }
}
