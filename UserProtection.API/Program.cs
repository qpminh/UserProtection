using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserProtection.API.Middlewares;
using UserProtection.Application.Dependency;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Dependency;
using UserProtection.Infrastructure.Helpers;
using UserProtection.Infrastructure.SeedData;

namespace UserProtection.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration["Gemini:ApiKey"] = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            // Add controllers
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Swagger + JWT support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UserProtection API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập JWT token vào đây. Ví dụ: Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Service DI
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            // Identity
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

            // VNPay helper
            builder.Services.AddSingleton<VnPayHelper>(sp =>
            {
                var config = builder.Configuration.GetSection("VnPay");
                return new VnPayHelper(
                    config["TmnCode"]!,
                    config["HashSecret"]!,
                    config["VnpUrl"]!,
                    config["ReturnUrl"]!
                );
            });

            // JWT Config
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // ==================== Port for Render ====================
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://0.0.0.0:{port}");
            Console.WriteLine($"✅ Server is running on http://0.0.0.0:{port}");

            // ==================== Database seeding ====================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<UserProtectionContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                await DbInitializer.SeedDefaultAdminAsync(services);
                await RoleSeeder.SeedRolesAsync(roleManager, logger);
                await UserSeeder.SeedUsersAsync(services);
                await PlanSeeder.SeedAsync(context);
                await BlogSeeder.SeedAsync(context);
                await SecuritySeeder.SeedAsync(context);
                await SubscriptionSeeder.SeedAsync(services);
                await SeedUserSubscriptionPaymentSeeder.SeedAsync(context, userManager);
            }

            // Middleware
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowAllOrigins");

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<ApiKeyMiddleware>();

            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapMethods("/health", new[] { "GET", "HEAD" }, () => Results.Ok("Healthy"));

            app.MapMethods("/health-db", new[] { "GET", "HEAD" }, async (UserProtectionContext db) =>
            {
                try
                {
                    var isDbAlive = await db.Database.CanConnectAsync();
                    return Results.Ok(new { db = isDbAlive });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.Run();
        }
    }
}
