using ChatUserLink.Services.Interfaces;
using Common.DataAccess;
using Common.DataAccess.SharedEntities.Links;
using Common.DataAccess.SharedEntities.Objects;
using Microsoft.EntityFrameworkCore;

namespace ChatUserLink.Services.Implementations;

public class UserAssociationRepository(UserDbContext userDbContext) : IUserAssociationRepository
{
    public async Task<bool> CheckUserKnowsAboutConversation(Guid userId, Guid conversationId)
    {
        return await userDbContext
            .UserConversations
            .AnyAsync(x => x.UserId == userId && x.ConversationId == conversationId);
    }

    public async Task<bool> CheckUserOwnsMessage(Guid userId, Guid messageId)
    {
        return await userDbContext
            .UserMessages
            .AnyAsync(x => x.UserId == userId && x.MessageId == messageId);
    }

    public async Task<IList<ConversationMessageEntity>> GetMessagesByConversationId(Guid conversationId)
    {
        return await userDbContext
            .ConversationMessages
            .Where(x => x.ConversationId == conversationId)
            .ToArrayAsync();
    }
    
    public async Task<IList<UserConversationEntity>> GetConversationsByUserId(Guid userId)
    {
        return await userDbContext
            .UserConversations
            .Where(x => x.UserId == userId)
            .ToArrayAsync();
    }

    public async Task Add(UserConversationEntity userConversationEntity)
    {
        userDbContext.UserConversations.Add(userConversationEntity);
        await userDbContext.SaveChangesAsync();
    }

    public Task Add(UserMessageEntity userMessageEntity)
    {
        userDbContext.UserMessages.Add(userMessageEntity);
        return userDbContext.SaveChangesAsync();
    }
}
