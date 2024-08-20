using Core.Common.DataAccess.SharedEntities.Users;
using Identities.DataAccess.Interfaces;
using Identities.Services.Factories;

namespace Identities.SQRS
{
    public class CreateIdentityQuery
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IdentityFactory _identityFactory;

        public CreateIdentityQuery(IIdentityRepository identityRepository, IdentityFactory identityFactory)
        {
            _identityRepository = identityRepository;
            _identityFactory = identityFactory;
        }
        
        public async Task<IdentityEntity> Execute(CancellationToken token)
        {
            IdentityEntity entity = _identityFactory.Create();

            await _identityRepository.Add(entity, token);
            
            return entity;
        }
    }
}
