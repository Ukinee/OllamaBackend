using Chat.DataAccess.Interfaces;
using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    [Obsolete("Rewrite", true)]
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

        public IList<MessageEntity> Execute(ConversationEntity conversation, int page, int pageSize)
        {
            return conversation
                .Messages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
