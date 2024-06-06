namespace Authorization.Domain.Extensions
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserCreateRequest createRequest, string hashedPassword, string salt) =>
            new UserEntity()
            {
                Name = createRequest.Name,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                HashedPassword = hashedPassword,
                Salt = salt,
                ConversationIds = [],
            };

        public static UserViewModel ToViewModel(this UserEntity request) =>
            new UserViewModel()
            {
                Name = request.Name,
                Id = request.Id,
                ConversationIds = request.ConversationIds,
            };
    }
}
