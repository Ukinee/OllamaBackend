﻿using Chat.CQRS.Commands;
using Chat.CQRS.Queries.Done;
using Chat.Domain.Conversations;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.CQRS.Queries.Done;

namespace Chat.Services.Implementations
{
    public class ConversationsService
    (
        GetPersonaQuery getPersonaQuery,
        CreateConversationQuery createConversationQuery,
        AddConversationCommand addConversationCommand,
        GetConversationQuery getConversationQuery,
        UpdateConversationCommand updateConversationCommand,
        ConvertConversationToViewModelQuery convertConversationToViewModelQuery,
        GetConversationPaginationQuery getConversationPaginationQuery,
        GetConversationsFromPersonaQuery getConversationsFromPersonaQuery
    )
    {
        public async Task<ConversationViewModel> Add(PostConversationRequest request, CancellationToken token)
        {
            PersonaEntity persona = await getPersonaQuery.Execute(request.PersonaId, token);
            ConversationEntity conversation = createConversationQuery.Execute(persona, request);
            await addConversationCommand.Execute(conversation, token);

            return convertConversationToViewModelQuery.Execute(conversation);
        }

        public async Task<ConversationViewModel> Update(PutConversationRequest request, CancellationToken token)
        {
            ConversationEntity conversation = await getConversationQuery.Execute(request.Id);
            await updateConversationCommand.Execute(conversation, request);

            return convertConversationToViewModelQuery.Execute(conversation);
        }

        public async Task<ConversationViewModel> GetPaginatedMessages(Guid guid, int routePage, int pageSize)
        {
            ConversationEntity conversation = await getConversationQuery.Execute(guid);

            return getConversationPaginationQuery.Execute(conversation, routePage, pageSize);
        }

        public async Task<IList<ConversationViewModel>> GetConversationsByPersona(Guid personaId, CancellationToken token)
        {
            PersonaEntity persona = await getPersonaQuery.Execute(personaId, token);
            
            return getConversationsFromPersonaQuery.Execute(persona);
        }
    }
}