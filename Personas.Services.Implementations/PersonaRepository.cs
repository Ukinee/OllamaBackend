using System.Linq.Expressions;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Users;
using Microsoft.EntityFrameworkCore;
using Personas.Services.Interfaces;

namespace Personas.Services.Implementations;

public class PersonaRepository : IPersonaRepository
{
    private readonly CompositeContext _userContext;

    public PersonaRepository(CompositeContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<PersonaEntity?> Find(Expression<Func<PersonaEntity, bool>> predicate, CancellationToken token)
    {
        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .FirstOrDefaultAsync(predicate, token);
    }

    public async Task<IEnumerable<PersonaEntity>> FindAll(Expression<Func<PersonaEntity, bool>> predicate)
    {
        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .Where(predicate)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<PersonaEntity>> FindMany(int amount, Expression<Func<PersonaEntity, bool>> predicate)
    {
        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .Where(predicate)
            .Take(amount)
            .ToArrayAsync();
    }

    public async Task Add(PersonaEntity personaEntity, CancellationToken token)
    {
        _userContext.Personas.Add(personaEntity);

        await Save(token);
    }

    public async Task Save(CancellationToken token)
    {
        await _userContext.SaveChangesAsync(token);
    }
}
