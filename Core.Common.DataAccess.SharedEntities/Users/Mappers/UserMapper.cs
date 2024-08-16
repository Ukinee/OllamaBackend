using Authorization.Domain;

namespace Core.Common.DataAccess.SharedEntities.Users.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserCreateRequest createRequest, PersonaEntity emptyPersona, string userType)
        {
            return new UserEntity
            {
                UserName = createRequest.UserName,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Role = userType,
                Personas = [emptyPersona],
            };
        }

        public static UserViewModel ToViewModel(this UserEntity entity, string token)
        {
            string name = string.IsNullOrWhiteSpace(entity.UserName) == false
                ? entity.UserName
                : throw new ArgumentNullException(nameof(entity.UserName));

            return new UserViewModel
            {
                UserName = name,
                Id = entity.Id,
                Token = token,
                PersonasIds = entity.Personas.Select(x => x.Id).ToList(),
            };
        }
    }
}
