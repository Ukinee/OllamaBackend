using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class CheckUserOwnsMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public CheckUserOwnsMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Execute(Guid messageId, Guid userId)
        {
            MessageEntity? message = await _messageRepository.Get(messageId);
            
            if(message == null)
                throw new NotFoundException(nameof(message));

            return message.SenderId == userId;
        }
    }
}
