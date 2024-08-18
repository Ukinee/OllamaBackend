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

    public async Task<PersonaEntity?> Find(Func<PersonaEntity, bool> predicate, CancellationToken token)
    {
        Expression<Func<PersonaEntity, bool>> expression = x => predicate.Invoke(x);

        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .FirstOrDefaultAsync(expression, token);
    }

    public async Task<IEnumerable<PersonaEntity>> FindAll(Func<PersonaEntity, bool> predicate)
    {
        Expression<Func<PersonaEntity, bool>> expression = x => predicate.Invoke(x);

        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .Where(expression)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<PersonaEntity>> FindMany(int amount, Func<PersonaEntity, bool> predicate)
    {
        Expression<Func<PersonaEntity, bool>> expression = x => predicate.Invoke(x);

        return await _userContext
            .Personas
            .Include(persona => persona.Conversations)
            .Include(persona => persona.Identity)
            .Where(expression)
            .Take(amount)
            .ToArrayAsync();
    }

    public async Task Add(PersonaEntity personaEntity)
    {
        _userContext.Personas.Add(personaEntity);
        
        await Save();
    }

    public async Task Save()
    {
        await _userContext.SaveChangesAsync();
    }
}
