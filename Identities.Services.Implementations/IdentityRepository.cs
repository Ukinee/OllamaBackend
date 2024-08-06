using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Identities.Services.Interfaces;

namespace Identities.Services.Implementations
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserContext _dbContext;

        public IdentityRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(IdentityEntity identity)
        {
            _dbContext.Identities.Add(identity);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task Link(IdentityEntity identity, PersonaEntity persona)
        {
            identity.Persona = persona;
            identity.PersonaId = persona.Id;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}

