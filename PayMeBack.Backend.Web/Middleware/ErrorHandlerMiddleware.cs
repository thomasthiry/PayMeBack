using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using PayMeBack.Backend.Models;
using System.Threading.Tasks;

namespace PayMeBack.Backend.Web.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (SecurityException ex)
            {
                context.Response.StatusCode = 403;
            }
        }
    }
}