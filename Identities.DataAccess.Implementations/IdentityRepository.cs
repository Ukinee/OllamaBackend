﻿using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.DataAccess.Interfaces;
using Identities.Models;

namespace Identities.DataAccess.Implementations
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly CompositeContext _dbContext;

        public IdentityRepository(CompositeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(IdentityEntity identity, CancellationToken token)
        {
            _dbContext.Identities.Add(identity);

            await _dbContext.SaveChangesAsync(token);
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
