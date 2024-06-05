using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OllamaBackend
{
    public static class BuilderFactory
    {
        public static WebApplicationBuilder Create(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Database context
            builder.Services.AddDbContext<MainRepository>
            (
                options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); }
            );

            builder
                .Services
                .AddScoped<IConversationRepository, ConversationRepository>()
                .AddScoped<IMessageRepository, MessageRepository>();

            return builder;
        }
    }
}
