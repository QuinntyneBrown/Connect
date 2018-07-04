using Connect.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.DateTime;
using static System.Security.Claims.ClaimValueTypes;

namespace Connect.Core.Identity
{
    public class SecurityTokenFactory : ISecurityTokenFactory
    {
        private readonly IConfiguration _configuration;
        public SecurityTokenFactory(IConfiguration configuration)
            => _configuration = configuration;
        
        public string Create(string uniqueName, ICollection<string> roles = default(ICollection<string>))
        {
            var now = UtcNow;
            
            var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, uniqueName),
                    new Claim(JwtRegisteredClaimNames.Sub, uniqueName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(now).ToUnixTimeSeconds()}", Integer64),
                };
            
            if(roles != default(ICollection<string>))
                foreach(var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role,ClaimValueTypes.String, _configuration["Authentication:JwtIssuer"]));
            
            var jwt = new JwtSecurityToken(
                issuer: _configuration["Authentication:JwtIssuer"],
                audience: _configuration["Authentication:JwtAudience"],
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(Convert.ToInt16(_configuration["Authentication:ExpirationMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:JwtKey"])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }        
    }
}
