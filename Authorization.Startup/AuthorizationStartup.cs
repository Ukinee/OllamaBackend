using System.Text;
using Authorization.Services.Factories;
using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Startup
{
    public static class AuthorizationStartup
    {
        public static IServiceCollection ConfigureAuthorization
            (this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IUserCreationService, UserCreationService>()
                .AddScoped<UserManager<UserEntity>>()
                .AddScoped<UserFactory>()
                .SetupJwt(configuration)
                .SetupIdentity();
        }

        private static IServiceCollection SetupIdentity(this IServiceCollection services)
        {
            IdentityBuilder identityBuilder = services
                .AddIdentity<UserEntity, IdentityRole<Guid>>
                (
                    options =>
                    {
                        options.Password.RequireDigit = false; //todo : hardcode
                        options.Password.RequireLowercase = false; //todo : hardcode
                        options.Password.RequireNonAlphanumeric = false; //todo : hardcode
                        options.Password.RequireUppercase = false; //todo : hardcode
                        options.Password.RequiredLength = 6; //todo : hardcode
                    }
                )
                .AddEntityFrameworkStores<CompositeContext>();

            return services;
        }

        private static IServiceCollection SetupJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorization()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer
                (
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Jwt:Issuer"], //todo : hardcode
                            ValidateAudience = true,
                            ValidAudience = configuration["Jwt:Audience"], //todo : hardcode
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            IssuerSigningKey = new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"])), //todo : hardcode
                        };
                    }
                );

            return services;
        }
    }
}
