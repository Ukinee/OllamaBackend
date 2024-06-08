using Authorization.Domain;
using Authorization.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Domain.Conversations.Mappers;
using Chat.Services.Interfaces;
using Common.DataAccess;

namespace Chat.CQRS.Queries
{
    public class GetGeneralConversationsWithUserIdQuery
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _userRepository;

        public GetGeneralConversationsWithUserIdQuery(IConversationRepository conversationRepository, IUserRepository userRepository)
        {
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
        }
        
        public async Task<IList<GeneralConversationViewModel>> Execute(Guid userId)
        {
            UserEntity? user = await _userRepository.Get(userId);

            if (user == null)
                throw new NotFoundException(nameof(user));
         
            List<ConversationEntity> conversations = await _conversationRepository.GetGeneralConversations(user.ConversationIds);
            
            return conversations.Select(x => x.ToGeneralConversation()).ToList();
        }
    }
}
