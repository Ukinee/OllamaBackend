﻿using Core.Common.DataAccess.SharedEntities.Users;
using Identities.Models;

namespace Identities.DataAccess.Interfaces
{
    public interface IIdentityRepository
    {
        public Task Add(IdentityEntity identity, CancellationToken token);
        public Task Update(PutIdentityRequest request, Guid id);
    }
}
