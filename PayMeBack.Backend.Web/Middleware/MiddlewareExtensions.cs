using Microsoft.AspNet.Builder;

namespace PayMeBack.Backend.Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthenticationMiddleware>();
        }
    }
}
