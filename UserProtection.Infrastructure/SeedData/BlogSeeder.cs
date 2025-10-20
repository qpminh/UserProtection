using Microsoft.EntityFrameworkCore;
using UserProtection.Domain.Entities;

namespace UserProtection.Infrastructure.SeedData
{
    public static class BlogSeeder
    {
        public static async Task SeedAsync(UserProtectionContext context)
        {
            await context.Database.MigrateAsync();

            // ================= SEED BLOGS =================
            if (!await context.Blogs.AnyAsync())
            {
                var blogs = new List<Blog>
                {
                    new Blog
                    {
                        Title = "Understanding Cyber Hygiene: 7 Daily Habits to Stay Secure",
                        Slug = "understanding-cyber-hygiene",
                        Summary = "Learn simple daily habits to strengthen your personal cybersecurity and protect your data online.",
                        Content = "In today's digital world, practicing good cyber hygiene is essential. From using strong passwords to enabling multi-factor authentication, these small steps can drastically reduce your exposure to threats...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2020/05/01/17/49/cyber-security-5117538_960_720.jpg",
                        Tags = "cybersecurity, habits, protection",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "Top 10 Data Privacy Tips for 2025",
                        Slug = "top-10-data-privacy-tips-2025",
                        Summary = "Data privacy isn't just for tech experts — here’s how anyone can protect their digital footprint in 2025.",
                        Content = "With online tracking becoming more advanced, understanding how to protect your data has never been more important. Start by reviewing your app permissions, clearing browser data, and avoiding public Wi-Fi for sensitive transactions...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2018/07/04/19/29/security-3518710_960_720.jpg",
                        Tags = "privacy, data, tips",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "AI in Cyber Defense: How Machine Learning Detects Threats",
                        Slug = "ai-in-cyber-defense",
                        Summary = "Explore how AI and machine learning technologies are revolutionizing cybersecurity threat detection.",
                        Content = "Artificial Intelligence plays a growing role in analyzing network traffic, detecting anomalies, and identifying threats before they cause harm. In this article, we’ll explore real-world examples of AI-driven protection systems...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2023/03/10/12/35/artificial-intelligence-7841824_960_720.jpg",
                        Tags = "AI, cybersecurity, machine learning",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "Password Managers: Friend or Foe?",
                        Slug = "password-managers-friend-or-foe",
                        Summary = "Are password managers really safe? Let’s explore their pros, cons, and best practices for using them securely.",
                        Content = "Password managers are an effective tool for handling complex passwords, but they also introduce single-point-of-failure risks. Choosing a reputable tool and enabling MFA can greatly reduce your vulnerability...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2016/11/29/13/12/password-1865746_960_720.jpg",
                        Tags = "passwords, security, management",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "The Future of Digital Identity: Beyond Passwords",
                        Slug = "future-of-digital-identity",
                        Summary = "From biometrics to passkeys — discover how authentication is evolving in a passwordless future.",
                        Content = "The next decade will redefine digital identity. With FIDO2 and passkeys, we’re moving away from traditional passwords to faster, safer, and more convenient login experiences...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2020/03/13/16/45/fingerprint-4922801_960_720.jpg",
                        Tags = "identity, biometrics, authentication",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "Remote Work Security: How to Protect Your Home Network",
                        Slug = "remote-work-security-home-network",
                        Summary = "Working from home introduces new security risks — here’s how to secure your router, Wi-Fi, and devices.",
                        Content = "The shift to remote work made home networks a new attack surface. Ensuring router firmware updates, using WPA3 encryption, and limiting IoT exposure are key steps to stay safe...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2020/04/15/14/05/home-office-5040808_960_720.jpg",
                        Tags = "remote work, home security, wifi",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "Cybersecurity Career Paths: Where to Start in 2025",
                        Slug = "cybersecurity-career-paths-2025",
                        Summary = "Interested in cybersecurity? Here are the most in-demand roles and certifications for new professionals.",
                        Content = "Cybersecurity offers a wide range of roles — from penetration testers and analysts to policy consultants. In 2025, certifications like CompTIA Security+, CEH, and CISSP remain valuable entry points...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2015/01/08/18/26/startup-593327_960_720.jpg",
                        Tags = "career, cybersecurity, education",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "Social Engineering Explained: Don’t Get Fooled",
                        Slug = "social-engineering-explained",
                        Summary = "Learn how hackers manipulate people — and how you can defend yourself against psychological attacks.",
                        Content = "Social engineering relies on exploiting human psychology rather than technical flaws. Recognizing manipulation tactics like urgency, authority, or fear can help you avoid scams and phishing attempts...",
                        ThumbnailUrl = "https://cdn.pixabay.com/photo/2016/11/29/11/49/fraud-1868725_960_720.jpg",
                        Tags = "social engineering, phishing, awareness",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.Blogs.AddRangeAsync(blogs);
                await context.SaveChangesAsync();
            }
        }
    }
}
