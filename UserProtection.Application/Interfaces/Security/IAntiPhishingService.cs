namespace UserProtection.Application.Interfaces.Security
{
    public interface IAntiPhishingService
    {
        bool IsSuspicious(string url, out string matchedPattern);
    }
}
