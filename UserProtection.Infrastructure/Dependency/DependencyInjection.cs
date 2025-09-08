using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;
using UserProtection.Infrastructure.Repositories.Core;

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
            return services;
        }
    }
}
