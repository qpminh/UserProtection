using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.SeedData
{
    public static class SubscriptionSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UserProtectionContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.MigrateAsync();

            // Kiểm tra nếu đã có subscription rồi thì bỏ qua
            if (await context.Subscriptions.AnyAsync())
                return;

            // Lấy danh sách user
            var users = await userManager.Users.ToListAsync();
            if (!users.Any())
                return;

            // Lấy các gói
            var basicPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Basic Plan");
            var standardPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Standard Plan");
            var premiumPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Premium Plan");

            if (basicPlan == null || standardPlan == null || premiumPlan == null)
                throw new Exception("Plans not found — please run PlanSeeder first.");

            var subscriptions = new List<Subscription>();

            foreach (var user in users)
            {
                Plan? selectedPlan = null;

                // Gán gói dựa theo role hoặc tên người dùng
                if (await userManager.IsInRoleAsync(user, "Customer"))
                    selectedPlan = basicPlan;
                else if (await userManager.IsInRoleAsync(user, "Manager"))
                    selectedPlan = standardPlan;
                else if (await userManager.IsInRoleAsync(user, "Employee"))
                    selectedPlan = standardPlan;
                else if (await userManager.IsInRoleAsync(user, "AdminCompany"))
                    selectedPlan = premiumPlan;

                if (selectedPlan == null) continue;

                var subscription = new Subscription
                {
                    UserId = user.Id,
                    PlanId = selectedPlan.PlanId,
                    Status = "Active",
                    AutoRenew = true,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1)
                };

                subscriptions.Add(subscription);
            }

            if (subscriptions.Any())
            {
                await context.Subscriptions.AddRangeAsync(subscriptions);
                await context.SaveChangesAsync();
            }
        }
    }
}
