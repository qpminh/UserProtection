using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Middlewares
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequireFeatureAccessAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _featureId;

        public RequireFeatureAccessAttribute(int featureId)
        {
            _featureId = featureId;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Subscription? subscription = null;

            // 1. Ưu tiên lấy từ ApiKeyMiddleware
            if (context.HttpContext.Items.TryGetValue("Subscription", out var subObj) && subObj is Subscription subFromKey)
            {
                subscription = subFromKey;
            }
            else
            {
                // 2. Fallback user login (JWT/Cookie)
                var userId = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var db = context.HttpContext.RequestServices.GetRequiredService<UserProtectionContext>();
                    subscription = await db.Subscriptions
                        .Include(s => s.Plan)
                            .ThenInclude(p => p.PlanFeatures)
                                .ThenInclude(pf => pf.Feature)
                        .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");
                }
            }

            if (subscription == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // 3. Kiểm tra feature
            var hasFeature = subscription.Plan.PlanFeatures
                .Any(f => f.FeatureId == _featureId);

            if (!hasFeature)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
