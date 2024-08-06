using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;

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
            
            if (conversation.Personas.Any(x => x.UserId == userId) == false)
                throw new UnauthorizedAccessException("You don't have permission to access this conversation");
            
            return conversation
                .Messages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
