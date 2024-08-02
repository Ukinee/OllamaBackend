using Authorization.Services.Factories;
using Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;

namespace Persona.DomainServices;

public class PersonaMapper
{
    private readonly IdentityFactory _identityFactory;
    private readonly PersonaFactory _personaFactory;

    public PersonaMapper(IdentityFactory identityFactory, PersonaFactory personaFactory)
    {
        _identityFactory = identityFactory;
        _personaFactory = personaFactory;
    }

    public PersonaEntity ToEntity(PostPersonaRequest createPersonaRequest, Guid userId)
    {
        IdentityEntity identity = _identityFactory.Create();
        PersonaEntity persona = _personaFactory.Create(userId, identity, createPersonaRequest.Name);

        return persona;
    }

    public PersonaViewModel ToViewModel(PersonaEntity entity)
    {
        return new PersonaViewModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            ConversationIds = entity
                .Conversations
                .Select(x => x.Id)
                .ToList(),
        };
    }
}
