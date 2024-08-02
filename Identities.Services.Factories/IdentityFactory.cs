using Common.DataAccess.SharedEntities.Users;

namespace Identities.Services.Factories
{
    public class IdentityFactory
    {
        public IdentityEntity Create()
        {
            return new IdentityEntity()
            {
                Id = Guid.NewGuid(),
                Description = "Default Identity", //todo: hardcode
                Habits = "Default Identity", //todo: hardcode
                PhysicalAttributes = "Default Identity", //todo: hardcode
            };
        }
    }
}
