using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.DataAccess;
using Chat.Services.Implementations;
using Chat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.CQRS;

namespace Chat.Startup
{
    public static class ChatStartup
    {
        public static IServiceCollection ConfigureChat(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IConversationRepository, ConversationRepository>()
                .AddScoped<IMessageRepository, MessageRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<DeleteConversationCommand>()
                .AddScoped<AddConversationToUserCommand>()
                .AddScoped<AddConversationQuery>()
                .AddScoped<GetConversationQuery>()
                .AddScoped<GetGeneralConversationsWithUserIdQuery>()
                .AddDbContext<ChatDbContext>
                (
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                );
        }
    }
}
