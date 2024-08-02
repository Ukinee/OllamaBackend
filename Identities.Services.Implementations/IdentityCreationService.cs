using Common.DataAccess.SharedEntities.Users;
using Identities.Services.Factories;
using Identities.Services.Interfaces;

namespace Identities.Services.Implementations
{
    public class IdentityCreationService : IIdentityCreationService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IdentityFactory _identityFactory;

        public IdentityCreationService(IIdentityRepository identityRepository, IdentityFactory identityFactory)
        {
            _identityRepository = identityRepository;
            _identityFactory = identityFactory;
        }
        
        public IdentityEntity Create()
        {
            IdentityEntity identity = _identityFactory.Create();
            _identityRepository.Add(identity);

            return identity;
        }
    }
}
