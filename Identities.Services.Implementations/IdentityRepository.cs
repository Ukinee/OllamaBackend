using Common.DataAccess;
using Common.DataAccess.SharedEntities.Users;
using Identities.Models;
using Identities.Services.Interfaces;

namespace Identities.Services.Implementations
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly CompositeContext _dbContext;

        public IdentityRepository(CompositeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(IdentityEntity identity)
        {
            _dbContext.Identities.Add(identity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(PutIdentityRequest request, Guid id)
        {
            IdentityEntity? identity = await _dbContext
                .Identities
                .FindAsync(id);

            if (identity is null)
            {
                throw new InvalidOperationException($"Identity {id} not found.");
            }
            
            identity.Description = request.Description;
            identity.PhysicalAttributes = request.PhysicalAttributes;
            identity.Habits = request.Habits;
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
