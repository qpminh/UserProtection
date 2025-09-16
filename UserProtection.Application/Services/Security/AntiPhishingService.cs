using System.Text.RegularExpressions;
using UserProtection.Application.Interfaces.Security;

namespace UserProtection.Application.Services.Security
{
    public class AntiPhishingService : IAntiPhishingService
    {
        // 1 số regex / rule cơ bản (thực tế có thể lấy từ DB AntiPhishingPattern)
        private readonly List<string> _suspiciousPatterns = new()
        {
            @"login.*\.ru",       // login domain lạ
            @"free.*gift",        // scam quà
            @"paypal.*verify",    // giả mạo paypal
        };

        public bool IsSuspicious(string url, out string matchedPattern)
        {
            foreach (var pattern in _suspiciousPatterns)
            {
                if (Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase))
                {
                    matchedPattern = pattern;
                    return true;
                }
            }
            matchedPattern = string.Empty;
            return false;
        }
    }
}
