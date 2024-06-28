using Persona.Models;

namespace Personas.Services.Interfaces
{
    public interface IPersonaLinkRepository
    {
        public Task Add(PersonaLinkEntity entity);
        public Task<PersonaLinkEntity?> Get(Guid conversationId, Guid userId);
        public Task Update(PersonaLinkEntity entity);
        public Task Delete(Guid userId, Guid conversationId);
    }
}
