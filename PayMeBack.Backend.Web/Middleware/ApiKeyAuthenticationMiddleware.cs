using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using PayMeBack.Backend.Contracts.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayMeBack.Backend.Web.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private IUserService _userService;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains("Authorization") == false || context.Request.Headers["Authorization"][0].StartsWith("Bearer:") == false)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var token = context.Request.Headers["Authorization"][0].Substring("Bearer:".Length).Trim();
            var user = _userService.GetUserForToken(token);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));

            await _next.Invoke(context);
        }
    }
}