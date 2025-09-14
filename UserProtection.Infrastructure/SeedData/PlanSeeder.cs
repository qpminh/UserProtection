using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.SeedData
{
    public static class PlanSeeder
    {
        public static async Task SeedAsync(UserProtectionContext context)
        {
            await context.Database.MigrateAsync();

            // ================= SEED FEATURES =================
            if (!await context.Features.AnyAsync())
            {
                var features = new List<Feature>
                {
                    new Feature { Name = "Anti-Phishing Protection", Description = "Detects and blocks phishing attempts" },
                    new Feature { Name = "Malware Scanner", Description = "Scans files and links for malware" },
                    new Feature { Name = "Data Encryption", Description = "Encrypts sensitive user data" },
                    new Feature { Name = "Real-time Threat Alerts", Description = "Notify users immediately of threats" },
                    new Feature { Name = "Cloud Backup", Description = "Secure cloud storage for critical data" }
                };
                await context.Features.AddRangeAsync(features);
                await context.SaveChangesAsync();
            }

            // ================= SEED COURSES =================
            if (!await context.Courses.AnyAsync())
            {
                var courses = new List<Course>
                {
                    new Course
                    {
                        Title = "Cybersecurity Basics",
                        Description = "Introduction to basic security practices",
                        Category = "Security",
                        Level = "Beginner",
                        Price = 0,
                        Status = "Active",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Course
                    {
                        Title = "Intermediate Security Awareness",
                        Description = "Covers common attacks and defenses",
                        Category = "Security",
                        Level = "Intermediate",
                        Price = 49.99M,
                        Status = "Active",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Course
                    {
                        Title = "Advanced Threat Protection",
                        Description = "In-depth course on advanced security threats",
                        Category = "Security",
                        Level = "Advanced",
                        Price = 99.99M,
                        Status = "Active",
                        CreatedAt = DateTime.UtcNow
                    }
                };
                await context.Courses.AddRangeAsync(courses);
                await context.SaveChangesAsync();
            }

            // ================= SEED PLANS =================
            if (!await context.Plans.AnyAsync())
            {
                var plans = new List<Plan>
                {
                    new Plan { Name = "Basic Plan", Description = "Essential protection", Price = 9.99M, BillingCycle = "Monthly", IsActive = true },
                    new Plan { Name = "Standard Plan", Description = "More security and learning", Price = 19.99M, BillingCycle = "Monthly", IsActive = true },
                    new Plan { Name = "Premium Plan", Description = "Full features and advanced courses", Price = 29.99M, BillingCycle = "Monthly", IsActive = true }
                };
                await context.Plans.AddRangeAsync(plans);
                await context.SaveChangesAsync();
            }

            // ================= MAP FEATURES & COURSES =================
            var allFeatures = await context.Features.ToListAsync();
            var allCourses = await context.Courses.ToListAsync();
            var basicPlan = await context.Plans.FirstAsync(p => p.Name == "Basic Plan");
            var standardPlan = await context.Plans.FirstAsync(p => p.Name == "Standard Plan");
            var premiumPlan = await context.Plans.FirstAsync(p => p.Name == "Premium Plan");

            // Basic Plan → 1 Feature + 1 Course
            await AddPlanFeaturesAsync(context, basicPlan.PlanId, allFeatures.Where(f => f.Name == "Anti-Phishing Protection"));
            await AddPlanCoursesAsync(context, basicPlan.PlanId, allCourses.Where(c => c.Title == "Cybersecurity Basics"));

            // Standard Plan → 3 Features + 2 Courses
            await AddPlanFeaturesAsync(context, standardPlan.PlanId, allFeatures.Where(f => f.Name == "Anti-Phishing Protection" || f.Name == "Malware Scanner" || f.Name == "Data Encryption"));
            await AddPlanCoursesAsync(context, standardPlan.PlanId, allCourses.Where(c => c.Title == "Cybersecurity Basics" || c.Title == "Intermediate Security Awareness"));

            // Premium Plan → All Features + All Courses
            await AddPlanFeaturesAsync(context, premiumPlan.PlanId, allFeatures);
            await AddPlanCoursesAsync(context, premiumPlan.PlanId, allCourses);

            await context.SaveChangesAsync();
        }

        private static async Task AddPlanFeaturesAsync(UserProtectionContext context, int planId, IEnumerable<Feature> features)
        {
            foreach (var feature in features)
            {
                if (!await context.PlanFeatures.AnyAsync(pf => pf.PlanId == planId && pf.FeatureId == feature.FeatureId))
                {
                    await context.PlanFeatures.AddAsync(new PlanFeature { PlanId = planId, FeatureId = feature.FeatureId });
                }
            }
        }

        private static async Task AddPlanCoursesAsync(UserProtectionContext context, int planId, IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                if (!await context.PlanCourses.AnyAsync(pc => pc.PlanId == planId && pc.CourseId == course.CourseId))
                {
                    await context.PlanCourses.AddAsync(new PlanCourse { PlanId = planId, CourseId = course.CourseId });
                }
            }
        }
    }
}
