using Persona.Models.Personas;

namespace Personas.Services.Interfaces;

public interface IPersonaRepository
{
    public Task<PersonaEntity?> Get(Guid id);
    public Task Add(PersonaEntity personaEntity);
    public Task Update(PutPersonaRequest request);
    public Task Delete(Guid id);
    
    public Task<PersonaEntity[]> GetAll(Guid userId);
}
