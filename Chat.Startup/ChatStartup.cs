﻿using Authorization.Services.Implementations;
using Authorization.Services.Interfaces;
using Chat.CQRS.Commands;
using Chat.CQRS.Queries;
using Chat.CQRS.Queries.Done;
using Chat.DataAccess.Implementations;
using Chat.DataAccess.Interfaces;
using Chat.Services.Implementations;
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
                .AddScoped<GetConversationPaginationQuery>()
                .AddScoped<GetMessagesQuery>()
                .AddScoped<AddMessageQuery>()
                .AddScoped<ConversationsService>()
                .AddScoped<CreateConversationQuery>()
                .AddScoped<AddConversationCommand>()
                .AddScoped<UpdateConversationCommand>()
                .AddScoped<ConvertConversationToGeneralViewModelQuery>()
                .AddScoped<GetConversationPaginationQuery>()
                .AddScoped<MessagesService>()
                .AddScoped<GetConversationsFromPersonaQuery>()
                .AddScoped<AddConversationCommand>()
                .AddScoped<UpdateConversationCommand>()
                .AddScoped<GetConversationQuery>()
                .AddScoped<GetUserQuery>();
        }
    }
}
