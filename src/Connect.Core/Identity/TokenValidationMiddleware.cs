using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System.Threading.Tasks;

namespace Connect.Core.Identity
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        
        public TokenValidationMiddleware(RequestDelegate next)
            => _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            var repository = httpContext.RequestServices.GetService<IAccessTokenRepository>();
            var validAccessTokens = await repository.GetValidAccessTokenValuesAsync();

            if (httpContext.User.Identity.IsAuthenticated
                && !httpContext.Request.Path.Value.StartsWith("/hub")
                && !validAccessTokens.Contains(httpContext.Request.GetAccessToken()))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized");
            }
            else
                await _next.Invoke(httpContext);
        }
    }
}
