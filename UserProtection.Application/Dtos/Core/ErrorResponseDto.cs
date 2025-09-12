using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProtection.Application.Dtos.Core
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
