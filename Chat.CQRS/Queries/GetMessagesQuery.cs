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

        public async Task<IList<MessageEntity>> Execute(Guid conversationId, int page, int pageSize)
        {
            ConversationEntity? conversation = await _conversationRepository.GetConcreteConversation(conversationId);

            if (conversation == null)
                throw new KeyNotFoundException("Conversation not found");
            
            return conversation
                .Messages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
