using InterviewsApp.Core.Interfaces;
using InterviewsApp.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewsApp.Core.Services
{
    public class JwtService : IAuthService
    {
        private readonly IOptions<AuthSettings> _authSettings;
        public JwtService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings;
        }

        public string Generate(string login)
        {
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = DateTime.UtcNow.AddHours(_authSettings.Value.LifeTimeHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
