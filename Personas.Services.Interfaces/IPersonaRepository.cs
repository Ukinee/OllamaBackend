using System.Linq.Expressions;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Personas.Services.Interfaces
{
    public interface IPersonaRepository
    {
        public Task<PersonaEntity?> Find(Expression<Func<PersonaEntity, bool>> predicate, CancellationToken token);
        public Task<IEnumerable<PersonaEntity>> FindAll(Expression<Func<PersonaEntity, bool>> predicate);
        public Task<IEnumerable<PersonaEntity>> FindMany(int amount, Expression<Func<PersonaEntity, bool>> predicate);
        
        public Task Add(PersonaEntity personaEntity, CancellationToken token);
        public Task Save(CancellationToken token);
    }
}
