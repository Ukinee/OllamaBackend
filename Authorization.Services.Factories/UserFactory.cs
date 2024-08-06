using Authorization.Domain;
using Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Factories
{
    public class UserFactory
    {
        public UserEntity Create(UserCreateRequest createRequest, string userType)
        {
            return new UserEntity()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UserName = createRequest.UserName,
                Role = userType,
                Personas = [],
            };
        }
    }
}
