using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations;

public class PersonasRepository : IPersonasRepository
{
    private readonly CompositeContext _userContext;

    public PersonasRepository(CompositeContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<PersonaEntity?> Get(Guid id)
    {
        return await _userContext.Personas
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(PersonaEntity personaEntity)
    {
        _userContext.Personas.Add(personaEntity);
        await SaveChangesAsync();
    }

    public async Task Update(PutPersonaRequest request, Guid id)
    {
        PersonaEntity? personaEntity = await _userContext.Personas
            .FirstOrDefaultAsync(x => x.Id == id);

        if (personaEntity is null)
            throw new Exception("Persona not found");

        personaEntity.Name = request.Name;

        await SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        PersonaEntity? personaEntity = await _userContext.Personas
            .Include(persona => persona.User)
            .ThenInclude(entity => entity.Personas)
            .FirstOrDefaultAsync(persona => persona.Id == id);

        if (personaEntity is null)
            throw new InvalidOperationException("Persona not found");
        
        if(personaEntity.User.Personas.Count == 1)
            throw new InvalidOperationException("Can't delete last persona");

        personaEntity.IsDeleted = true;
        await SaveChangesAsync();
    }

    public async Task<PersonaEntity[]> GetAll(Guid userId)
    {
        PersonaEntity[] personas = await _userContext
            .Personas
            .Where(x => x.UserId == userId && x.IsDeleted == false)
            .ToArrayAsync();

        return personas;
    }

    private async Task SaveChangesAsync()
    {
        await _userContext.SaveChangesAsync();
    }
}
