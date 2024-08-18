using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;

namespace Chat.CQRS.Queries.Done
{
    public class ConvertConversationToViewModelQuery
    {
        public ConversationViewModel Execute(ConversationEntity conversation)
        {
            return conversation.ToViewModel();
        }
    }
}
