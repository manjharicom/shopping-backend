using shopping_backend.CustomMiddlewares;
using Microsoft.AspNetCore.Builder;

namespace shopping_backend.CustomMiddlewareExtensions
{
    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
