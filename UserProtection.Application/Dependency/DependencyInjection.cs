using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Interfaces;
using UserProtection.Application.Services.Core;
using UserProtection.Application.Services.Security;
using UserProtection.Infrastructure.Interfaces;
using UserProtection.Infrastructure.Repositories;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrustedLinkService, TrustedLinkService>();
            services.AddScoped<ISuspiciousLinkService, SuspiciousLinkService>();
            return services;
        }
    }
}
