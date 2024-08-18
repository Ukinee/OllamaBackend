using Chat.CQRS.Queries;
using Chat.CQRS.Queries.Done;
using Chat.Domain.Messages;
using Core.Common.DataAccess.SharedEntities.Users;
using Persona.CQRS.Queries.Done;

namespace Chat.Services.Implementations
{
    public class MessagesService
    {
        private readonly GetPersonaQuery _getPersonaQuery;
        private readonly AddMessageQuery _addMessageQuery;

        public MessagesService(GetPersonaQuery getPersonaQuery, AddMessageQuery addMessageQuery)
        {
            _getPersonaQuery = getPersonaQuery;
            _addMessageQuery = addMessageQuery;
        }

        public async Task<MessageViewModel> AddMessage(PostMessageRequest request, CancellationToken token)
        {
            PersonaEntity personaEntity = await _getPersonaQuery.Execute(request.PersonaId, token);

            return await _addMessageQuery.Handle(request, personaEntity);
        }
    }
}
