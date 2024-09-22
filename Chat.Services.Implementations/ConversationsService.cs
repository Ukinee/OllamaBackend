using Chat.CQRS.Commands;
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
        ConvertConversationToGeneralViewModelQuery convertConversationToGeneralViewModelQuery,
        GetConversationPaginationQuery getConversationPaginationQuery,
        GetConversationsFromPersonaQuery getConversationsFromPersonaQuery
    )
    {
        public async Task<GeneralConversationViewModel> Add(PostConversationRequest request, CancellationToken token)
        {
            PersonaEntity persona = await getPersonaQuery.Execute(request.PersonaId, token);
            ConversationEntity conversation = createConversationQuery.Execute(persona, request);
            await addConversationCommand.Execute(conversation, token);

            return convertConversationToGeneralViewModelQuery.Execute(conversation);
        }

        public async Task<GeneralConversationViewModel> Update(PutConversationRequest request, CancellationToken token)
        {
            ConversationEntity conversation = await getConversationQuery.Execute(request.Id, token);
            await updateConversationCommand.Execute(conversation, request);

            return convertConversationToGeneralViewModelQuery.Execute(conversation);
        }

        public async Task<ConversationViewModel> GetPaginatedMessages(Guid guid, int routePage, int pageSize, CancellationToken cancellationToken)
        {
            ConversationEntity conversation = await getConversationQuery.Execute(guid, cancellationToken);

            return getConversationPaginationQuery.Execute(conversation, routePage, pageSize);
        }

        public async Task<IList<GeneralConversationViewModel>> GetGeneralConversationsByPersona(Guid personaId, CancellationToken token)
        {
            PersonaEntity persona = await getPersonaQuery.Execute(personaId, token);
            
            return getConversationsFromPersonaQuery.Execute(persona);
        }
    }
}
