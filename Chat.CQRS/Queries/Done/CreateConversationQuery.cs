using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Chats.Mappers;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Chat.CQRS.Queries.Done
{
    public class CreateConversationQuery
    {
        public ConversationEntity Execute(PersonaEntity owner, PostConversationRequest postConversationRequest)
        {
            return postConversationRequest.ToEntity(owner);
        }
    }
}
