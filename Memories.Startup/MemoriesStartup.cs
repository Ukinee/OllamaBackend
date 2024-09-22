using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Memories.Startup;

public static class MemoriesStartup
{
    public static IServiceCollection ConfigureMemories(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
