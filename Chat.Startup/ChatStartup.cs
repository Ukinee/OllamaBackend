using Chat.Services.Implementations;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Startup;

public static class ChatStartup
{
    public static IServiceCollection ConfigureChat(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IConversationRepository, ConversationRepository>()
            .AddScoped<IMessageRepository, MessageRepository>()
            .AddDbContext<ChatDbContext>
            (
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
    }
}
