using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Connect.Core.Models;
using Connect.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Connect.Core.Identity
{
    public class AutoAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISecurityTokenFactory _tokenProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        
        public AutoAuthenticationMiddleware(
            ISecurityTokenFactory tokenProvider, 
            RequestDelegate next, 
            IServiceScopeFactory serviceScopeFactory) {            
            _next = next;
            _tokenProvider = tokenProvider;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IAccessTokenRepository>();
                var username = "quinntynebrown@gmail.com";
                var token = _tokenProvider.Create(username, new List<string>() { "Admin" });
                httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");
                repository.Add(new AccessToken() {
                    Value = token,
                    Username = username,
                    IsValid = true
                });
                await repository.SaveChangesAsync(default(CancellationToken));                
                await _next.Invoke(httpContext);
            }
        }
    }
}
