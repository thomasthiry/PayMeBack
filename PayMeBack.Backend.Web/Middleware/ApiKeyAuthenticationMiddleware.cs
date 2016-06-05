using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using PayMeBack.Backend.Contracts;
using PayMeBack.Backend.Contracts.Services;
using PayMeBack.Backend.Models;
using PayMeBack.Backend.Repository;
using PayMeBack.Backend.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayMeBack.Backend.Web.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            if (context.Request.Method != "OPTIONS" && context.Request.Path != "/login")
            {
                var token = GetBearerAuthenticationToken(context.Request.Headers);
                if (token == null)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var user = userService.GetUserForToken(token);

                if (user == null)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }

            await _next.Invoke(context);
        }

        private string GetBearerAuthenticationToken(IHeaderDictionary headers)
        {
            string header = null;
            string bearerToken = null;

            if (headers.Keys.Contains("Authentication"))
            {
                header = headers["Authentication"][0];
            }

            if (headers.Keys.Contains("authentication"))
            {
                header = headers["authentication"][0];
            }

            if (header != null && header.StartsWith("Bearer", System.StringComparison.CurrentCultureIgnoreCase))
            {
                bearerToken = header.Substring("Bearer".Length).Trim();
            }

            return bearerToken;
        }
    }
}