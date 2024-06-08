using Authorization.Setup;
using Chat.Startup;

namespace OllamaBackend
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
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
