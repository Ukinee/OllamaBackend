using Authorization.Startup;
using Chat.Startup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OllamaBackend2
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .ConfigureAuthorization(configuration)
                .ConfigureChat(configuration)
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
    }
}
