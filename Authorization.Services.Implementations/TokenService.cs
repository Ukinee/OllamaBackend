using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Services.Interfaces;
using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;

            _symmetricSecurityKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]) // todo: hardcoded 
            );
        }

        public async Task<string> CreateToken(UserManager<UserEntity> userManager, UserEntity user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            ];

            SigningCredentials credentials = new SigningCredentials
            (
                _symmetricSecurityKey,
                SecurityAlgorithms.HmacSha512Signature
            );

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // todo: hardcoded 
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"], // todo: hardcoded 
                Audience = _configuration["Jwt:Audience"], // todo: hardcoded 
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
