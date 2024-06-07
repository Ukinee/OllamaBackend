namespace Authorization.Domain.Extensions
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserCreateRequest createRequest) =>
            new UserEntity()
            {
                UserName = createRequest.UserName,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ConversationIds = [],
            };

        public static UserViewModel ToViewModel(this UserEntity entity, string token) =>
            new UserViewModel()
            {
                UserName = entity.UserName ?? throw new NullReferenceException(nameof(entity.UserName)),
                Id = entity.Id,
                Token = token,
                ConversationIds = entity.ConversationIds,
            };
    }
}
