using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;

namespace Chat.CQRS.Queries
{
    public class GetMessagesQuery
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;

        public GetMessagesQuery
        (
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository
        )
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
        }

        public async Task<IList<MessageEntity>> Execute(Guid conversationId, Guid userId, int page, int pageSize)
        {
            ConversationEntity? conversation = await _conversationRepository.Get(conversationId);

            if (conversation == null)
                throw new KeyNotFoundException("Conversation not found");
            
            if (conversation.OwnerId != userId && conversation.Participants.Contains(userId) == false)
                throw new UnauthorizedAccessException("You don't have permission to access this conversation");
            
            List<Guid> messageIds = conversation
                .Messages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return await _messageRepository.Get(messageIds);
        }
    }
}
