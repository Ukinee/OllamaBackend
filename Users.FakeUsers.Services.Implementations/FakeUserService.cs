using Chat.CQRS.Queries;
using Chat.CQRS.Queries.Done;
using Chat.Domain.Messages;
using Users.FakeUsers.Services.Interfaces;

namespace Users.FakeUsers.Services.Implementations;

public class FakeUserService : IFakeUserService
{
    private static readonly Guid SystemPersonaId = new Guid("26f17c33-a1c1-4cdf-8383-936325c3b388"); // TODO hardcode
    
    private readonly AddMessageQuery _addMessageQuery;

    public FakeUserService(AddMessageQuery addMessageQuery)
    {
        _addMessageQuery = addMessageQuery;
    }

    public async Task SendSystemMessage(string content, Guid conversationId)
    {
        PostMessageRequest postMessageRequest = new PostMessageRequest()
        {
            Content = content,
            ConversationId = conversationId,
            PersonaId = SystemPersonaId,
            Images = [],
        };
        
        await _addMessageQuery.Handle(postMessageRequest);
    }
}
