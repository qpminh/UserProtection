using Microsoft.Extensions.DependencyInjection;
using UserProtection.Application.Interfaces;
using UserProtection.Application.Map;
using UserProtection.Application.Services.Core;
using UserProtection.Application.Services.Security;
using UserProtection.Application.Services.Payment;
using UserProtection.Application.Services.Course;
using UserProtection.Application.Services.Feature;
using UserProtection.Application.Services.Plan;
using UserProtection.Application.Services.Subscription;

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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<PlanService>();
            services.AddScoped<SubscriptionService>();
            services.AddScoped<SubscriptionKeyService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<FeatureService>();
            services.AddScoped<PlanFeatureService>();
            services.AddScoped<CourseService>();
            services.AddScoped<PlanCourseService>();
            services.AddScoped<ITrustedLinkService, TrustedLinkService>();
            services.AddScoped<ISuspiciousLinkService, SuspiciousLinkService>();
            services.AddScoped<AntiPhishingService>();

            return services;
        }
    }
}
