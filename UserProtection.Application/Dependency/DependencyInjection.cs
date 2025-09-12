using Microsoft.Extensions.DependencyInjection;
using UserProtection.Application.Interfaces;
using UserProtection.Application.Map;
using UserProtection.Application.Services.Core;
using UserProtection.Application.Services.Payment;

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
            services.AddScoped<PaymentService>();

            return services;
        }
    }
}
