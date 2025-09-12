using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Middlewares
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequireFeatureAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _featureName;

        public RequireFeatureAttribute(string featureName)
        {
            _featureName = featureName;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var db = context.HttpContext.RequestServices.GetRequiredService<UserProtectionContext>();

            // Lấy userId từ JWT Claims
            var userId = context.HttpContext.User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var subscription = await db.Subscriptions
                .Include(s => s.Plan)
                    .ThenInclude(p => p.PlanFeatures)
                        .ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (subscription == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Check Feature
            var hasFeature = subscription.Plan.PlanFeatures.Any(f => f.Feature.Name == _featureName);

            if (!hasFeature)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
