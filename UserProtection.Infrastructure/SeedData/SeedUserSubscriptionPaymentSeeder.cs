using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;
using UserProtection.Domain.Constants;

namespace UserProtection.Infrastructure.SeedData
{
    public static class SeedUserSubscriptionPaymentSeeder
    {
        public static async Task SeedAsync(
            UserProtectionContext context,
            UserManager<User> userManager)
        {
            await context.Database.MigrateAsync();

            var userList = new List<(string First, string Last, string Email, string Phone)>
            {
                ("Nam", "Nguyễn Sơn", "namsn@fpt.edu.vn", "0962896727"),
                ("Hưng", "Nguyễn Quý", "hungnqse183854@fpt.edu.vn", "0964478714"),
                ("Trung", "Nguyễn Thế", "trungntse183179@fpt.edu.vn", "0913173378"),
                ("Nhã", "Trần Kim", "nhatkse182290@fpt.edu.vn", "0933102278"),
                ("Sang", "Nguyễn Hữu", "sangnhse182627@fpt.edu.vn", "0853836162"),

                ("Quyền", "Phạm Quốc", "quyenqpse180426@fpt.edu.vn", "0887426621"),
                ("Bảo", "Quách Gia", "baogqse180449@fpt.edu.vn", "0918827345"),
                ("Thành Tín", "Lê Hữu", "tintlhse180481@fpt.edu.vn", "0937261844"),
                ("Nguyên", "Trần Phạm Thảo", "nguyenpttse180486@fpt.edu.vn", "0962847711"),
                ("Trang", "Trần Thị Thanh", "trangtttse180491@fpt.edu.vn", "0973815526"),

                ("Kiệt", "Trần Gia", "kietgtsse180500@fpt.edu.vn", "0837162249"),
                ("Dương", "Phan Khánh", "duongkpse180524@fpt.edu.vn", "0896673152"),
                ("Nam", "Phan Thành", "namtpse180525@fpt.edu.vn", "0908736124"),
                ("Phong", "Hoàng Gia", "phongghse180543@fpt.edu.vn", "0938427719"),
                ("Thắng", "Bùi Minh", "thangmbse180564@fpt.edu.vn", "0827136644"),

                ("Phước", "Đào Công An", "phuoccadse180581@fpt.edu.vn", "0856147733"),
                ("Quân", "Nguyễn Anh", "quanaanse180619@fpt.edu.vn", "0986723155"),
                ("Việt", "Nguyễn", "vietnse180672@fpt.edu.vn", "0937612281"),
                ("An", "Hoàng Quốc", "anquhse181520@fpt.edu.vn", "0967442288"),
                ("Thịnh", "Trần Đình", "thinhdtse181531@fpt.edu.vn", "0917326684"),

                ("Ánh", "Đỗ Long", "anhldse181818@fpt.edu.vn", "0976614277"),
                ("Thắng", "Trần Quốc", "thangqtse181868@fpt.edu.vn", "0908731662"),
                ("Đào", "Phạm Thị Anh", "daotatpse181924@fpt.edu.vn", "0897713666"),
                ("Trí", "Nguyễn Thanh", "trittnse182028@fpt.edu.vn", "0938846625"),
                ("Huy", "Đỗ Quốc", "huyqdse182535@fpt.edu.vn", "0827714499")
            };

            var basicPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Basic Plan");
            if (basicPlan == null)
            {
                Console.WriteLine("⚠ Basic Plan not found.");
                return;
            }

            foreach (var (first, last, email, phone) in userList)
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = email,
                        Email = email,
                        FirstName = first,
                        LastName = last,
                        PhoneNumber = phone,
                        CreatedAt = DateTime.UtcNow,
                        Status = "Active"
                    };

                    var createResult = await userManager.CreateAsync(user, "Customer@123");
                    if (!createResult.Succeeded)
                    {
                        Console.WriteLine($"❌ Failed to create user {email}");
                        continue;
                    }

                    Console.WriteLine($"✓ Created user: {email}");
                }

                var existingSub = await context.Subscriptions
                    .FirstOrDefaultAsync(s => s.UserId == user.Id && s.PlanId == basicPlan.PlanId);

                if (existingSub != null)
                {
                    Console.WriteLine($"→ Subscription already exists for {email}, skipping.");
                    continue;
                }

                var startDate = RandomDateInPast3Months();
                var endDate = startDate.AddMonths(1);

                var subscription = new Subscription
                {
                    UserId = user.Id,
                    PlanId = basicPlan.PlanId,
                    Status = SubscriptionStatus.Active,
                    StartDate = startDate,
                    EndDate = endDate,
                    AutoRenew = false
                };

                await context.Subscriptions.AddAsync(subscription);
                await context.SaveChangesAsync(); 

                Console.WriteLine($"✓ Subscription created for {email}");

                var paymentDate = startDate.AddHours(new Random().Next(1, 12)); 

                var payment = new Payment
                {
                    SubscriptionId = subscription.SubscriptionId,
                    Amount = basicPlan.Price,
                    Status = PaymentStatus.Succeeded,
                    PaymentMethod = "Manual",
                    PaymentDate = paymentDate,
                    TransactionId = $"SEED-{Guid.NewGuid():N}",
                    FrontendReturnUrl = null
                };

                await context.Payments.AddAsync(payment);
                await context.SaveChangesAsync();

                Console.WriteLine($"✓ Payment created for {email}");
            }

            Console.WriteLine("🎉 SEED COMPLETED SUCCESSFULLY");
        }

        private static DateTime RandomDateInPast3Months()
        {
            var rng = new Random();
            int daysBack = rng.Next(0, 90);     // random 0–90 ngày
            int hoursBack = rng.Next(0, 24);    // random giờ
            int minutesBack = rng.Next(0, 60);  // random phút

            return DateTime.UtcNow
                .AddDays(-daysBack)
                .AddHours(-hoursBack)
                .AddMinutes(-minutesBack);
        }
    }
}
