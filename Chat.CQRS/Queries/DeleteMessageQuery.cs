using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess;
using Core.Common.DataAccess.SharedEntities.Chats;

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
