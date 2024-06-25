using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using ChatUserLink.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;

namespace Chat.CQRS.Queries
{
    public class GetGeneralConversationsWithUserIdQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserAssociationRepository _userAssociationRepository;

        public GetGeneralConversationsWithUserIdQuery
        (
            IConversationRepository conversationRepository,
            IUserAssociationRepository userAssociationRepository
        )
        {
            _conversationRepository = conversationRepository;
            _userAssociationRepository = userAssociationRepository;
        }

        public async Task<IList<GeneralConversationViewModel>> Execute(Guid userId)
        {
            IList<UserConversationEntity> userConversations = await _userAssociationRepository.GetConversationsByUserId(userId);
            Guid[] conversationIds = userConversations.Select(x => x.ConversationId).ToArray();
            List<ConversationEntity> conversations = await _conversationRepository.Get(conversationIds);

            return conversations.Select(x => x.ToGeneralConversation()).ToList();
        }
    }
}
