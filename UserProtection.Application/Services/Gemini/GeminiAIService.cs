using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using UserProtection.Application.Interfaces.Gemini;

namespace UserProtection.Application.Services.Gemini
{
    public class GeminiAIService : IAIService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public GeminiAIService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
            // Cấu hình BaseAddress chung (tùy chọn)
            // _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
        }

        public async Task<string> AskGeminiAsync(string prompt)
        {
            var apiKey = _config["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                return "Lỗi cấu hình: API Key của Gemini không được tìm thấy.";
            }

            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            // 1. ĐỊNH NGHĨA JSON SCHEMA BẮT BUỘC
            //schema mô tả cấu trúc JSON muốn mô hình trả về.
            var responseSchema = new
            {
                type = "object",
                properties = new
                {
                    Title = new { type = "string", description = "Tiêu đề ngắn gọn cho việc đánh giá lừa đảo." },
                    Description = new { type = "string", description = "Mô tả chi tiết lý do và cơ sở để đánh giá lừa đảo." },
                    PatternType = new { type = "string", description = "Loại hình lừa đảo (ví dụ: 'Lừa đảo đe dọa', 'Lừa đảo văn bản hành chính', 'Lừa đảo giải thưởng', 'Lừa đảo hỗ trợ kỹ thuật', 'Gian lận hóa đơn')." },
                    Safe = new { type = "boolean", description = "Kết luận an toàn. True nếu an toàn, False nếu là lừa đảo hoặc rủi ro cao." }
                },
                required = new[] { "Title", "Description", "PatternType", "Safe" }
            };

            // 2. CHUẨN BỊ PAYLOAD VỚI HƯỚNG DẪN HỆ THỐNG VÀ RESPONSE SCHEMA
            var systemInstruction = "Bạn là một API phân tích an ninh mạng. Nhiệm vụ của bạn là đánh giá một thông báo (thường là email) và trả về kết luận dưới dạng JSON. Phản hồi của bạn PHẢI tuân thủ chính xác JSON Schema được cung cấp, KHÔNG kèm theo bất kỳ văn bản hoặc ký tự markdown nào (ví dụ: KHÔNG có ```json).";

            var userPrompt = $"[{systemInstruction}]\n\nPhân tích và đánh giá thông báo sau dưới góc độ rủi ro lừa đảo, sau đó trả về kết quả tuân thủ JSON Schema:\n\n{prompt}";

            var payload = new
            {
                generationConfig = new
                {
                    // Thiết lập để buộc đầu ra là JSON
                    responseMimeType = "application/json",
                    // Cung cấp schema để Gemini biết cấu trúc JSON mong muốn
                    responseSchema = responseSchema
                },
                contents = new[]
                {
            new
            {
                role = "user",
                parts = new[]
                {
                    // Gộp hướng dẫn hệ thống vào prompt để đảm bảo mô hình nhận được nó
                    new { text = userPrompt }
                }
            }
        },
            };

            // 3. GỬI YÊU CẦU API
            var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = false });

            var response = await _httpClient.PostAsync(endpoint,
                new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"Lỗi HTTP {response.StatusCode}. Vui lòng kiểm tra lại endpoint hoặc API Key. Chi tiết: {errorContent}";
            }

            var json = await response.Content.ReadAsStringAsync();
            using var root = JsonDocument.Parse(json);
            var rootElement = root.RootElement;

            // 4. TRÍCH XUẤT PHẢN HỒI
            if (rootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
            {
                if (candidates[0].TryGetProperty("content", out var content) &&
                    content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                {
                    if (parts[0].TryGetProperty("text", out var textElement))
                    {
                        var rawText = textElement.GetString() ?? "Phản hồi rỗng từ mô hình.";

                        // KHÔNG cần bước làm sạch (như loại bỏ ```json) nữa
                        // vì việc sử dụng responseMimeType="application/json" đã đảm bảo đầu ra là JSON thuần.

                        return rawText;
                    }
                }
            }

            return "Không thể trích xuất phản hồi hợp lệ từ Gemini. Phản hồi JSON API: " + json;
        }
    }
}