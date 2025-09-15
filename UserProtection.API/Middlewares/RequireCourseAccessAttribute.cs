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
    public class RequireCourseAccessAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _courseId;

        public RequireCourseAccessAttribute(int courseId)
        {
            _courseId = courseId;
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
                            .ThenInclude(p => p.PlanCourses)
                                .ThenInclude(pc => pc.Course)
                        .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");
                }
            }

            if (subscription == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // 3. Kiểm tra course
            var hasCourse = subscription.Plan.PlanCourses
                .Any(c => c.CourseId == _courseId);

            if (!hasCourse)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
