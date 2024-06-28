using Persona.Models.Personas;

namespace Persona.Models.Mappers
{
    public static class PersonaMappers
    {
        public static PersonaViewModel ToViewModel(this PersonaEntity persona)
        {
            return new PersonaViewModel
            {
                Id = persona.Id,
                ChatName = persona.ChatName,
                ChatRole = persona.ChatRole,
                Description = persona.Description,
            };
        }
        
        public static PersonaEntity ToEntity(this PostPersonaRequest createPersonaRequest)
        {
            return new PersonaEntity
            {
                Id = Guid.NewGuid(),
                ChatName = createPersonaRequest.ChatName,
                ChatRole = createPersonaRequest.ChatRole,
                Description = createPersonaRequest.Description
            };
        }
        
        public static PersonaLinkEntity ToEntity(this PostPersonaLinkRequest createPersonaLinkRequest, Guid userId)
        {
            return new PersonaLinkEntity
            {
                UserId = userId,
                PersonaId = createPersonaLinkRequest.PersonaId,
                ConversationId = createPersonaLinkRequest.ConversationId
            };
        }
        
        public static PersonaLinkEntity ToEntity(this PutPersonaLinkRequest createPersonaLinkRequest, Guid userId)
        {
            return new PersonaLinkEntity
            {
                UserId = userId,
                PersonaId = createPersonaLinkRequest.PersonaId,
                ConversationId = createPersonaLinkRequest.ConversationId
            };
        }

        public static PersonaLinkViewModel ToViewModel(this PersonaLinkEntity personaLink)
        {
            return new PersonaLinkViewModel
            {
                UserId = personaLink.UserId,
                PersonaId = personaLink.PersonaId,
                ConversationId = personaLink.ConversationId
            };
        }
    }
}
