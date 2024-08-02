using Common.DataAccess.SharedEntities.Users;

namespace Identities.Services.Interfaces
{
    public interface IIdentityRepository
    {
        public Task Add(IdentityEntity identity);
    }
}
