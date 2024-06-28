using Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using Persona.Models.Personas;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations;

public class PersonasRepository : IPersonaRepository
{
    private readonly UserDbContext _userDbContext;

    public PersonasRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<PersonaEntity?> Get(Guid id)
    {
        return await _userDbContext.Personas
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Add(PersonaEntity personaEntity)
    {
        _userDbContext.Personas.Add(personaEntity);
        await Save();
    }

    public async Task Update(PutPersonaRequest request)
    {
        PersonaEntity? personaEntity = await _userDbContext.Personas
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (personaEntity is null)
            throw new Exception("Persona not found");

        personaEntity.Description = request.Description;
        personaEntity.ChatRole = request.ChatRole;
        personaEntity.ChatName = request.ChatName;

        await Save();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PersonaEntity[]> GetAll(Guid userId)
    {
        PersonaEntity[] personas = await _userDbContext
            .Personas
            .Where(x => x.OwnerId == userId)
            .ToArrayAsync();

        return personas;
    }

    private async Task Save()
    {
        await _userDbContext.SaveChangesAsync();
    }
}
