namespace UserProtection.Application.Interfaces.Gemini
{
    public interface IAIService
    {
        Task<string> AskGeminiAsync(string prompt);
    }
}
