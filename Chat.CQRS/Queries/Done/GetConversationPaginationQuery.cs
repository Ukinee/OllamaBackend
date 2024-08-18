using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;

namespace Chat.CQRS.Queries.Done
{
    public class GetConversationPaginationQuery
    {
        public ConversationViewModel Execute(ConversationEntity conversation, int page, int pageSize)
        {
            IList<MessageEntity> messages = conversation
                .Messages
                .OrderByDescending(x => x.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return conversation.ToViewModel(messages);
        }
    }
}
