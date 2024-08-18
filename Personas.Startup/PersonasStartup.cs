using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persona.CQRS.Queries;
using Persona.CQRS.Queries.Done;
using Persona.Domain.Services;
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
                    .AddScoped<IPersonaRepository, PersonaRepository>()
                    .AddScoped<IPersonaCreationService, PersonaCreationService>()
                    .AddScoped<PersonaFactory>()
                    .AddScoped<PersonaMapper>()
                    .AddScoped<CreatePersonaQuery>()
                    .AddScoped<GetPersonaQuery>()
                    .AddScoped<GetPersonasQuery>()
                    .AddScoped<UpdatePersonaQuery>()
                ;
        }
    }
}
