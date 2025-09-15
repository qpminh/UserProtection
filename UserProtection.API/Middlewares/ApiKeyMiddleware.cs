using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserProtectionContext db)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                await _next(context);
                return;
            }

            var value = authHeader.ToString();

            // Nếu là JWT Bearer token -> bỏ qua để JwtMiddleware xử lý
            if (value.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Nếu là ApiKey
            if (value.StartsWith("ApiKey ", StringComparison.OrdinalIgnoreCase))
            {
                var apiKey = value.Substring("ApiKey ".Length).Trim();
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }

                var subscription = await db.Subscriptions
                    .Include(s => s.Plan)
                        .ThenInclude(p => p.PlanFeatures)
                            .ThenInclude(pf => pf.Feature)
                    .Include(s => s.Plan)
                        .ThenInclude(p => p.PlanCourses)
                            .ThenInclude(pc => pc.Course)
                    .Include(s => s.SubscriptionKeys)
                    .FirstOrDefaultAsync(s =>
                        s.SubscriptionKeys.Any(k => k.KeyValue == apiKey && k.IsActive) &&
                        s.Status == "Active");

                if (subscription == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid or expired API Key");
                    return;
                }

                context.Items["Subscription"] = subscription;
            }

            await _next(context);
        }
    }
}
