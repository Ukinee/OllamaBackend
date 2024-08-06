using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Chats;
using Common.DataAccess.SharedEntities.Chats.Mappers;

namespace Chat.CQRS.Queries
{
    public class GetGeneralConversationsWithUserIdQuery
    {
        private readonly IConversationRepository _conversationRepository;

        public GetGeneralConversationsWithUserIdQuery(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        
        public async Task<IList<GeneralConversationViewModel>> Execute(Guid personaId)
        {
            List<ConversationEntity> conversations = await _conversationRepository.GetGeneralConversations(personaId);
            
            return conversations.Select(x => x.ToGeneralConversation()).ToList();
        }
    }
}
