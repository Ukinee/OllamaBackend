using Chat.CQRS.Queries.Done;
using Chat.Domain.Messages;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.CQRS.Queries.Done;
using Users.FakeUsers.Services.Interfaces;

namespace Users.FakeUsers.Services.Implementations;

public class FakeUserService : IFakeUserService
{
    private static readonly Guid SystemPersonaId = new Guid("26f17c33-a1c1-4cdf-8383-936325c3b388"); // TODO hardcode
    
    private readonly AddMessageQuery _addMessageQuery;
    private readonly GetPersonaQuery _getPersonaQuery;

    public FakeUserService(AddMessageQuery addMessageQuery, GetPersonaQuery getPersonaQuery)
    {
        _addMessageQuery = addMessageQuery;
        _getPersonaQuery = getPersonaQuery;
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

        PersonaEntity personaEntity = await _getPersonaQuery.Execute(SystemPersonaId, CancellationToken.None);
        
        await _addMessageQuery.Handle(postMessageRequest, personaEntity);
    }
}
