using Authorization.Startup;
using Chat.Startup;
using Common.DataAccess;
using Identities.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Personas.Startup;

namespace OllamaBackend2
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDatabases(configuration)
                .ConfigureAuthorization(configuration)
                .ConfigureChat(configuration)
                .ConfigurePersonas(configuration)
                .ConfigureIdentities(configuration)
                .AddLogging
                (
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddDebug();
                    }
                );
        }

        private static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<CompositeContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<ChatContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<UserContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
