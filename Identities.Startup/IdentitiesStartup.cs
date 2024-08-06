using Identities.Services.Factories;
using Identities.Services.Implementations;
using Identities.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identities.Startup
{
    public static class IdentitiesStartup
    {
        public static IServiceCollection ConfigureIdentities(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                    .AddScoped<IIdentityRepository, IdentityRepository>()
                    .AddScoped<IIdentityCreationService, IdentityCreationService>()
                    .AddScoped<IdentityFactory>()
                ;
        }
    }
}
