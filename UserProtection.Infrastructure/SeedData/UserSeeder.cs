using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.SeedData
{
    public static class UserSeeder
    {
        // Danh sách user mẫu
        private static readonly List<(string Username, string Email, string Password, string FirstName, string LastName, string Role)> DefaultUsers = new()
        {
            ("customer1", "customer1@gmail.com", "Customer@123", "Nguyen", "Customer", "Customer"),
            ("manager1",  "manager1@gmail.com",  "Manager@123",  "Tran",   "Manager",  "Manager"),
            ("employee1", "employee1@gmail.com", "Employee@123", "Le",     "Employee", "Employee"),
            ("adminc1",   "adminc1@gmail.com",   "AdminC@123",   "Pham",   "Company",  "AdminCompany")
        };

        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var (username, email, password, firstName, lastName, role) in DefaultUsers)
            {
                // Tạo role nếu chưa có
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                // Check user tồn tại chưa
                var existingUser = await userManager.FindByNameAsync(username);
                if (existingUser != null) continue;

                var user = new User
                {
                    UserName = username,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = "0123456789",
                    Status = "Active",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var createResult = await userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                    throw new Exception($"Seed user {username} failed: " + string.Join("; ", createResult.Errors));

                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
