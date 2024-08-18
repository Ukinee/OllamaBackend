using Core.Common.DataAccess.SharedEntities.Users;
using Identities.Services.Factories;

namespace Identities.SQRS
{
    public class CreateIdentityQuery
    {
        private readonly IdentityFactory _identityFactory;

        public CreateIdentityQuery(IdentityFactory identityFactory)
        {
            _identityFactory = identityFactory;
        }
        
        public IdentityEntity Execute()
        {
            return _identityFactory.Create();
        }
    }
}
