using Authorization.Startup;
using Chat.Startup;
using Common.UserChatLinks.Startup;
using Core.Common.DataAccess;
using Identities.Startup;
using Memories.Startup;
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
                .ConfigureMemories(configuration)
                .ConfigurePersonas(configuration)
                .ConfigureUserChatLinks(configuration)
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
                .AddDbContext<CompositeContext>
                (
                    options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                );
        }
    }
}
