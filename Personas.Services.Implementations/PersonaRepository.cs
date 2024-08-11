using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations;

public class PersonaRepository : IPersonaRepository
{
    private readonly CompositeContext _userContext;

    public PersonaRepository(CompositeContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<PersonaEntity?> Get(Guid id)
    {
        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PersonaEntity?> GetWithConversations(Guid personaId)
    {
        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .FirstOrDefaultAsync(x => x.Id == personaId);
    }

    public async Task Add(PersonaEntity personaEntity)
    {
        _userContext.Personas.Add(personaEntity);
        await SaveChangesAsync();
    }

    public async Task Update(PutPersonaRequest request, Guid personaId)
    {
        PersonaEntity? personaEntity = await _userContext.Personas
            .FirstOrDefaultAsync(persona => persona.Id == personaId);

        ArgumentNullException.ThrowIfNull(personaEntity);

        personaEntity.Name = request.Name;

        await SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        PersonaEntity? personaEntity = await _userContext.Personas
            .Include(persona => persona.User)
            .ThenInclude(entity => entity.Personas)
            .FirstOrDefaultAsync(persona => persona.Id == id);

        ArgumentNullException.ThrowIfNull(personaEntity);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(personaEntity.User.Personas.Count, 1);
        
        personaEntity.IsDeleted = true;
        await SaveChangesAsync();
    }

    public async Task<PersonaEntity[]> GetAll(Guid userId)
    {
        PersonaEntity[] personas = await _userContext
            .Personas
            .Include(x => x.Conversations)
            .Where(x => x.UserId == userId)
            .ToArrayAsync();

        return personas;
    }

    private async Task SaveChangesAsync()
    {
        await _userContext.SaveChangesAsync();
    }
}
