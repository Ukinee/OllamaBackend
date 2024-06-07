using System.Text;
using Authorization.DataAccess;
using Authorization.Domain;
using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Setup;

public static class AuthorizationStartup
{
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<UserDbContext>
            (
                options => options.UseSqlServer(configuration.GetConnectionString("UserConnection"))
            )
            .AddScoped<ITokenService, TokenService>()
            .SetupJwt(configuration)
            .SetupIdentity();
    }

    private static IServiceCollection SetupIdentity(this IServiceCollection services)
    {
        IdentityBuilder identityBuilder = services.AddIdentity<UserEntity, IdentityRole<Guid>>
        (
            options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }
        );

        identityBuilder.AddEntityFrameworkStores<UserDbContext>();

        return services;
    }

    private static IServiceCollection SetupJwt(this IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationBuilder authenticationBuilder = services.AddAuthentication
        (
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        );

        authenticationBuilder.AddJwtBearer
        (
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"])),
                };
            }
        );
        
        return services;
    }
}
