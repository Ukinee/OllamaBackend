using Authorization.Domain;
using Common.DataAccess.SharedEntities.Users;

namespace Authorization.Services.Factories
{
    public class UserFactory
    {
        public UserEntity Create(Guid userId, UserCreateRequest createRequest, PersonaEntity defaultPersona, string userType)
        {
            return new UserEntity()
            {
                Id = userId,
                CreatedAt = DateTime.Now,
                UserName = createRequest.UserName,
                Role = userType,
                Personas = [defaultPersona],
            };
        }
    }
}
