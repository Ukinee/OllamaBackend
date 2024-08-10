using Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;

namespace Personas.Services.Interfaces
{
    public interface IPersonasRepository
    {
        public Task<PersonaEntity?> Get(Guid id);
        public Task<PersonaEntity?> GetWithConversations(Guid personaId);
        
        public Task Add(PersonaEntity personaEntity);
        public Task Update(PutPersonaRequest request, Guid personaId);
        public Task Delete(Guid id);
        public Task<PersonaEntity[]> GetAll(Guid userId);
    }
}
