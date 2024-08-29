using Chat.CQRS.Queries.Done;
using Common.UserChatLinks.CQRS;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.CQRS.Queries.Done;

namespace Common.UserChatLinks.Services;

public class UserChatLinkService
{
    private readonly GetPersonaQuery _getPersonaQuery;
    private readonly GetConversationQuery _getConversationQuery;
    private readonly AddPersonaToConversationCommand _addPersonaToConversationCommand;
    private readonly RemovePersonaFromConversationCommand _removePersonaFromConversationCommand;

    public UserChatLinkService
    (
        GetPersonaQuery getPersonaQuery,
        GetConversationQuery getConversationQuery,
        AddPersonaToConversationCommand addPersonaToConversationCommand,
        RemovePersonaFromConversationCommand removePersonaFromConversationCommand
    )
    {
        _getPersonaQuery = getPersonaQuery;
        _getConversationQuery = getConversationQuery;
        _addPersonaToConversationCommand = addPersonaToConversationCommand;
        _removePersonaFromConversationCommand = removePersonaFromConversationCommand;
    }

    public async Task AddPersona
    (
        Guid conversationId,
        Guid personaId,
        CancellationToken cancellationToken
    )
    {
        PersonaEntity persona = await _getPersonaQuery.Execute(personaId, cancellationToken);
        ConversationEntity conversation = await _getConversationQuery.Execute(conversationId, cancellationToken);
        await _addPersonaToConversationCommand.Execute(persona, conversation);
    }
    
    public async Task RemovePersona
    (
        Guid conversationId,
        Guid personaId,
        CancellationToken cancellationToken
    )
    {
        PersonaEntity persona = await _getPersonaQuery.Execute(personaId, cancellationToken);
        ConversationEntity conversation = await _getConversationQuery.Execute(conversationId, cancellationToken);
        await _removePersonaFromConversationCommand.Execute(persona, conversation);
    }
}
