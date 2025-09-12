using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;
using UserProtection.Infrastructure.Repositories.Core;
using UserProtection.Infrastructure.Repositories.Security;
using UserProtection.Infrastructure.Repositories.Payment;

namespace UserProtection.Infrastructure.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UserProtectionContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<UserProtectionContext>(provider =>
            //    provider.GetRequiredService<UserProtectionContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<ITrustedLinkRepository, TrustedLinkRepository>();
            services.AddScoped<ISuspiciousLinkRepository, SuspiciousLinkRepository>();
            return services;
        }
    }
}
