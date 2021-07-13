using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Auth
{
    public class AuthManager : IJwtAuthManager
    {
        public string GenerateToken(string name)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConstants.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var tokenDescription = new JwtSecurityToken(
                JWTConstants.Issuer,
                JWTConstants.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );
            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(tokenDescription),
                expiration = tokenDescription.ValidTo
            };

            return results.token;
        }
    }
}
