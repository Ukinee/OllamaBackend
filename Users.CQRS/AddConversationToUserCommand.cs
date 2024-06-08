using Authorization.Domain;
using Authorization.Services.Interfaces;

namespace Users.CQRS
{
    public class AddConversationToUserCommand
    {
        private readonly IUserRepository _userRepository;

        public AddConversationToUserCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(Guid userId, Guid conversationId)
        {
            UserEntity? userEntity = await _userRepository.Get(userId);

            if (userEntity == null)
                throw new Exception("User not found"); //TODO: create custom exception

            await _userRepository.AddConversationToUser(userEntity, conversationId);
        }
    }
}
