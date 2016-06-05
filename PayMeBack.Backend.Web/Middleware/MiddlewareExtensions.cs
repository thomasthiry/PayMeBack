using Microsoft.AspNet.Builder;

namespace PayMeBack.Backend.Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthenticationMiddleware>();
        }
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
