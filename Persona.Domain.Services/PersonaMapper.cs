using Core.Common.DataAccess.SharedEntities.Users;
using Identities.Services.Interfaces;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Persona.Domain.Services;

public class PersonaMapper
{
    private readonly IIdentityCreationService _identityCreationService;
    private readonly IPersonaCreationService _personaCreationService;

    public PersonaMapper(IIdentityCreationService identityCreationService, IPersonaCreationService personaCreationService)
    {
        _identityCreationService = identityCreationService;
        _personaCreationService = personaCreationService;
    }

    public async Task<PersonaEntity> CreateEntity(PostPersonaRequest createPersonaRequest)
    {
        IdentityEntity identity = await _identityCreationService.Create();
        PersonaEntity persona = await _personaCreationService.Create(createPersonaRequest.UserId, identity, createPersonaRequest.Name);

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
