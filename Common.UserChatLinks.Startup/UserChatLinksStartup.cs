using Common.UserChatLinks.CQRS;
using Common.UserChatLinks.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserPersonaLinks.CQRS;
using Users.FakeUsers.Services.Implementations;
using Users.FakeUsers.Services.Interfaces;

namespace Common.UserChatLinks.Startup;

public static class UserChatLinksStartup
{
    public static IServiceCollection ConfigureUserChatLinks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<AddPersonaToConversationCommand>()
            .AddScoped<RemovePersonaFromConversationCommand>()
            .AddScoped<LinkPersonaToUserCommand>()
            .AddScoped<UserChatLinkService>()
            .AddScoped<ISystemMessageService, SystemMessageService>()
            ;

        return services;
    }
}
