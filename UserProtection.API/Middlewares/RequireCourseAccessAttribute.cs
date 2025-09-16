using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;

namespace UserProtection.API.Middlewares
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RequireCourseAccessAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            List<Subscription>? subscriptions = null;

            // 1. Ưu tiên lấy từ ApiKeyMiddleware
            if (httpContext.Items.TryGetValue("Subscription", out var subObj) && subObj is Subscription subFromKey)
            {
                subscriptions = new List<Subscription> { subFromKey };
            }
            else
            {
                // 2. Fallback user login (JWT)
                var userId = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var db = httpContext.RequestServices.GetRequiredService<UserProtectionContext>();

                    subscriptions = await db.Subscriptions
                        .Include(s => s.Plan)
                            .ThenInclude(p => p.PlanCourses)
                        .Where(s => s.UserId == userId && s.Status == "Active")
                        .ToListAsync();
                }
            }

            if (subscriptions == null || !subscriptions.Any())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // 3. Lấy danh sách courseId từ route hoặc action arguments
            List<int> courseIds = new();

            // Trường hợp action có param "id"
            if (context.ActionArguments.TryGetValue("id", out var singleId) && singleId is int idVal)
            {
                courseIds.Add(idVal);
            }

            // Trường hợp action có param "ids" (List<int>)
            if (context.ActionArguments.TryGetValue("ids", out var listObj) && listObj is IEnumerable<int> idsList)
            {
                courseIds.AddRange(idsList);
            }

            // Trường hợp lấy từ route values
            if (!courseIds.Any() && httpContext.Request.RouteValues.TryGetValue("id", out var routeValue) &&
                int.TryParse(routeValue?.ToString(), out var routeId))
            {
                courseIds.Add(routeId);
            }

            if (!courseIds.Any())
            {
                context.Result = new BadRequestObjectResult(new { Message = "CourseId(s) are required" });
                return;
            }

            // 4. Kiểm tra quyền với tất cả courseIds
            var hasAllAccess = courseIds.All(courseId =>
                subscriptions.Any(s => s.Plan.PlanCourses.Any(pc => pc.CourseId == courseId)));

            if (!hasAllAccess)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
