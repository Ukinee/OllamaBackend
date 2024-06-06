using Authorization.DataAccess;
using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IValidationService, ValidationService>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddScoped<IUserRepository, UserRepository>()
            ;
    }
}
