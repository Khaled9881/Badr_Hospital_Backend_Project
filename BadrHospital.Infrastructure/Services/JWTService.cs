using BadrHospital.Application.Common;
using BadrHospital.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BadrHospital.Infrastructure.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;

        public JWTService(IConfiguration configuration)
        {
            _config = configuration;
        }


        public string CreateToken(TokenRequest tokenRequest)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, tokenRequest.UserId),
                new Claim(ClaimTypes.Email, tokenRequest.Email ?? ""),
                new Claim(Microsoft.IdentityModel.JsonWebTokens. JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in tokenRequest.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            //        var jwtKey = _config["Jwt:Key"]
            //          ??  throw new InvalidOperationException("Jwt:Key is not  configured.");
            //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            //        var expireMinutesRaw = _config["Jwt:ExpireMinutes"]
            //            ?? throw new InvalidOperationException("Jwt:ExpireMinutes is not configured.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(
                double.Parse(_config["Jwt:ExpireMinutes"]!)),
                SigningCredentials = creds
            };

            var tokenHandler = new JsonWebTokenHandler();
            string jwt = tokenHandler.CreateToken(tokenDescriptor);

            return jwt;

        }
    }
}
