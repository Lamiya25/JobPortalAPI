using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace JobPortalAPI.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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

              context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                _logger.LogError($" Status Code: {context.Response.StatusCode} Source: {ex.Source} Message: {ex.Message}");

                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Title = "Internal Error"
                };

                var errorResponseJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync( errorResponseJson);           
            }
        }
    }
}
