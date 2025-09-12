using System.Net;
using System.Text.Json;
using UserProtection.Application.Dtos.Core;

namespace UserProtection.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var response = context.Response;
                response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    InvalidOperationException => (int)HttpStatusCode.Conflict,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                response.StatusCode = statusCode;

                var error = new ErrorResponseDto(statusCode, ex.Message,
                    details: context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                        ? ex.StackTrace
                        : null);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                await response.WriteAsync(JsonSerializer.Serialize(error, options));
            }
        }
    }
}
