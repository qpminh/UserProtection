using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProtection.Application.Interfaces.Gemini
{
    public interface IAIService
    {
        Task<string> AskGeminiAsync(string prompt);
    }
}
