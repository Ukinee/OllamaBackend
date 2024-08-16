using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.DataAccess.Implementations;
using Chat.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<CheckUserOwnsMessageQuery>()
                .AddScoped<GetMessageQuery>()
                .AddScoped<GetConversationViewModelQuery>()
                .AddScoped<DeleteMessageQuery>()
                .AddScoped<GetMessagesQuery>()
                .AddScoped<AddMessageQuery>()
                .AddScoped<CheckUserInConversationQuery>()
                .AddScoped<AddConversationQuery>()
                .AddScoped<UpdateConversationCommand>()
                .AddScoped<GetConversationQuery>()
                .AddScoped<GetGeneralConversationsWithUserIdQuery>();
        }
    }
}
