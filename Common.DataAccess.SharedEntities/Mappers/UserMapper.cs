using Authorization.Domain;
using Common.DataAccess.SharedEntities.Objects;

namespace Common.DataAccess.SharedEntities.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserCreateRequest createRequest)
        {
            return new UserEntity
            {
                UserName = createRequest.UserName,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
            };
        }

        public static UserViewModel ToViewModel(this UserEntity entity, string token)
        {
            return new UserViewModel
            {
                UserName = entity.UserName ?? throw new NullReferenceException(nameof(entity.UserName)),
                Id = entity.Id,
                Token = token,
            };
        }
    }
}
