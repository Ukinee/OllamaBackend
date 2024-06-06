using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OllamaBackend
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IConversationRepository, ConversationRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddDbContext<ChatDbContext>
                (
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                );
    }
}
