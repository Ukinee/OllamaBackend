using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Domain;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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
            IList<string> roles = await userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? throw new NullReferenceException(nameof(user.UserName))),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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
                Audience = _configuration["Jwt:Audience"] // todo: hardcoded 
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
