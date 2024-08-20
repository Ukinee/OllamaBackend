using Core.Common.DataAccess.SharedEntities.Users;
using Persona.Models.Personas;

namespace Personas.Services.Interfaces
{
    public interface IPersonaRepository
    {
        public Task<PersonaEntity?> Find(Func<PersonaEntity, bool> predicate, CancellationToken token);
        public Task<IEnumerable<PersonaEntity>> FindAll(Func<PersonaEntity, bool> predicate);
        public Task<IEnumerable<PersonaEntity>> FindMany(int amount, Func<PersonaEntity, bool> predicate);
        
        public Task Add(PersonaEntity personaEntity, CancellationToken token);
        public Task Save(CancellationToken token);
    }
}
