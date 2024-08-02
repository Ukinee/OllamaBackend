using Chat.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class DeleteMessageQuery
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageQuery(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        public async Task Remove(Guid messageId)
        {
            MessageEntity? message = await _messageRepository.Get(messageId);
            
            if (message == null)
                throw new NotFoundException(nameof(message));
            
            await _messageRepository.Remove(message);
        }
    }
}
