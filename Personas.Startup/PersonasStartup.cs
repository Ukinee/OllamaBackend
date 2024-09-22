using Core.Common.DataAccess.SharedEntities.Users;
using Identities.SQRS;
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
using UserPersonaLinks.CQRS;

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
                    .AddScoped<PersonaService>()
                    .AddScoped<GetPersonasQuery>()
                    .AddScoped<GetPersonaQuery>()
                    .AddScoped<CreatePersonaQuery>()
                    .AddScoped<CreateIdentityQuery>()
                    .AddScoped<UpdatePersonaCommand>()
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
