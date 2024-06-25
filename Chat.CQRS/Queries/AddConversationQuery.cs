using Chat.Domain.Conversations;
using Chat.Services.Interfaces;
using ChatUserLink.Services.Interfaces;
using Common.DataAccess.SharedEntities;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Mappers;
using Common.DataAccess.SharedEntities.Objects;

namespace Chat.CQRS.Queries
{
    public class AddConversationQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserAssociationRepository _userAssociationRepository;

        public AddConversationQuery
        (
            IConversationRepository conversationRepository,
            IUserAssociationRepository userAssociationRepository
        )
        {
            _conversationRepository = conversationRepository;
            _userAssociationRepository = userAssociationRepository;
        }

        public async Task<GeneralConversationViewModel> Handle(PostConversationRequest conversation, Guid userId)
        {
            ConversationEntity conversationEntity = conversation.ToEntity();

            await _conversationRepository.Add(conversationEntity);
            
            UserConversationEntity link = new UserConversationEntity(userId, conversationEntity.Id);
            await _userAssociationRepository.Add(link);

            return conversationEntity.ToGeneralConversation();
        }
    }
}
