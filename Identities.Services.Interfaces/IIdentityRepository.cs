﻿using Common.DataAccess.SharedEntities.Users;
using Identities.Models;

namespace Identities.Services.Interfaces
{
    public interface IIdentityRepository
    {
        public Task Add(IdentityEntity identity);
        public Task Link(IdentityEntity identity, PersonaEntity persona);
        public Task Update(PutIdentityRequest request, Guid id);
    }
}