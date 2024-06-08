using Authorization.Domain;
using Authorization.Services.Interfaces;
using Common.DataAccess;

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
                throw new NotFoundException(nameof(userEntity));

            await _userRepository.AddConversationToUser(userEntity, conversationId);
        }
    }
}
