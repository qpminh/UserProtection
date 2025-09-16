namespace UserProtection.Application.Dtos.Cores
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? Details { get; set; }  // optional (stacktrace, inner)
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ErrorResponseDto(int statusCode, string error, string? details = null)
        {
            StatusCode = statusCode;
            Error = error;
            Details = details;
        }
    }
}
