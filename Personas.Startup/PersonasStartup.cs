using Core.Common.DataAccess.SharedEntities.Users;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persona.CQRS.Commands;
using Persona.CQRS.Queries;
using Persona.CQRS.Queries.Done;
using Persona.Models.Personas;
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
                    .ConfigureMapster()
                    .AddScoped<IPersonaRepository, PersonaRepository>()
                    .AddScoped<PersonaFactory>()
                    .AddScoped<CreatePersonaQuery>()
                    .AddScoped<GetPersonaQuery>()
                    .AddScoped<GetPersonasQuery>()
                    .AddScoped<UpdatePersonaCommand>()
                ;
        }

        private static IServiceCollection ConfigureMapster(this IServiceCollection serviceCollection)
        {
            TypeAdapterConfig<PersonaEntity, PersonaViewModel>
                .NewConfig()
                .Map
                (
                    viewModel => viewModel.ConversationIds,
                    entity => entity.Conversations.Select(x => x.Id)
                );

            return serviceCollection;
        }
    }
}
