using Core.Common.DataAccess.SharedEntities.Users;

namespace Persona.Services.Factories
{
    public class PersonaFactory
    {
        public PersonaEntity Create(Guid userId, IdentityEntity identity, string username)
        {
            return new PersonaEntity
            {
                Id = Guid.NewGuid(),
                Name = $"{username}_Persona", //todo: hardcode
                Identity = identity,
                IdentityId = identity.Id,
                Conversations = [],
                UserId = userId,
            };
        }
    }
}
