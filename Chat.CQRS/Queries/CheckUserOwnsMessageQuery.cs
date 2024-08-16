using Authorization.Services.Interfaces;
using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Chat.CQRS.Queries
{
    public class CheckUserOwnsMessageQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public CheckUserOwnsMessageQuery(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Execute(Guid messageId, Guid userId)
        {
            MessageEntity? message = await _messageRepository.Get(messageId);
            
            if(message == null)
                throw new NotFoundException(nameof(message));

            UserEntity? userEntity = await _userRepository.Get(userId);
            
            if(userEntity == null)
                throw new NotFoundException(nameof(userEntity));
            
            return userEntity.Personas.Any(x => x.UserId == message.SenderPersonaId);
        }
    }
}
