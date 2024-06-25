using Common.DataAccess.SharedEntities.Links;

namespace ChatUserLink.Services.Interfaces;

public interface IUserAssociationRepository
{
    public Task<bool> CheckUserKnowsAboutConversation(Guid userId, Guid conversationId);
    public Task<bool> CheckUserOwnsMessage(Guid userId, Guid messageId);
    public Task<IList<ConversationMessageEntity>> GetMessagesByConversationId(Guid conversationId);
    public Task<IList<UserConversationEntity>> GetConversationsByUserId(Guid userId);
    public Task Add(UserConversationEntity userConversationEntity);
    public Task Add(UserMessageEntity userConversationEntity);
}
