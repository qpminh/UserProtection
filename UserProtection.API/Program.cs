
using Microsoft.AspNetCore.Identity;
using UserProtection.Application.Dependency;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Dependency;
using UserProtection.Infrastructure.SeedData;

namespace UserProtection.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Service
            builder.Services.AddApplicationServices();

            //Respo
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<UserProtectionContext>()
            .AddDefaultTokenProviders();


            var app = builder.Build();

            // ==================== Database seeding ====================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<UserProtectionContext>();

                await DbInitializer.SeedDefaultAdminAsync(services);
                await RoleSeeder.SeedRolesAsync(roleManager, logger);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
