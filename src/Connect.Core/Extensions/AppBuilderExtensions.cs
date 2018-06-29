using Microsoft.AspNetCore.Builder;
using Connect.Core.Identity;

namespace Connect.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTokenValidation(this IApplicationBuilder app)
            => app.UseMiddleware<TokenValidationMiddleware>();
    }
}