using Core.Common.DataAccess.SharedEntities.Chats;

namespace Chat.CQRS.Queries
{
    public class GetMessagesQuery
    {
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
