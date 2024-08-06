using Common.DataAccess.SharedEntities.Users;

namespace Identities.Services.Interfaces
{
    public interface IIdentityCreationService
    {
        public Task<IdentityEntity> Create();
    }
}
