using ChatUserLink.Services.Interfaces;

namespace ChatUserLink.CQRS;

public class UserKnowAboutConversationQuery
{
    private readonly IUserAssociationRepository _userAssociationRepository;

    public UserKnowAboutConversationQuery(IUserAssociationRepository userAssociationRepository)
    {
        _userAssociationRepository = userAssociationRepository;
    }
    
    public async Task<bool> Execute(Guid conversationId, Guid userId) =>
        await _userAssociationRepository
            .CheckUserKnowsAboutConversation(conversationId, userId);
}
