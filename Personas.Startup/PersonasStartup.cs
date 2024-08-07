using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persona.Services.Factories;
using Personas.Services.Implementations;
using Personas.Services.Interfaces;

namespace Personas.Startup
{
    public static class PersonasStartup
    {
        public static IServiceCollection ConfigurePersonas(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                    .AddScoped<IPersonasRepository, PersonasRepository>()
                    .AddScoped<IPersonaCreationService, PersonaCreationService>()
                    .AddScoped<PersonaFactory>()
                ;
        }
    }
}
