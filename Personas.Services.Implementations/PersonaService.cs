using Chat.CQRS.Queries.Done;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.SQRS;
using Mapster;
using Persona.CQRS.Commands;
using Persona.CQRS.Queries;
using Persona.CQRS.Queries.Done;
using Persona.Models.Personas;
using UserPersonaLinks.CQRS;
using Users.CQRS;

namespace Personas.Services.Implementations
{
    public class PersonaService
    {
        private readonly GetUserQuery _getUserQuery;
        private readonly GetConversationQuery _getConversationQuery;
        private readonly GetPersonasQuery _getPersonasQuery;
        private readonly GetPersonaQuery _getPersonaQuery;
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly CreateIdentityQuery _createIdentityQuery;
        private readonly LinkPersonaToUserCommand _linkPersonaToUserCommand;
        private readonly UpdatePersonaCommand _updatePersonaCommand;

        public PersonaService
        (
            GetUserQuery getUserQuery,
            GetConversationQuery getConversationQuery,
            GetPersonasQuery getPersonasQuery,
            CreatePersonaQuery createPersonaQuery,
            CreateIdentityQuery createIdentityQuery,
            LinkPersonaToUserCommand linkPersonaToUserCommand,
            UpdatePersonaCommand updatePersonaCommand,
            GetPersonaQuery getPersonaQuery
        )
        {
            _getUserQuery = getUserQuery;
            _getConversationQuery = getConversationQuery;
            _getPersonasQuery = getPersonasQuery;
            _createPersonaQuery = createPersonaQuery;
            _createIdentityQuery = createIdentityQuery;
            _linkPersonaToUserCommand = linkPersonaToUserCommand;
            _updatePersonaCommand = updatePersonaCommand;
            _getPersonaQuery = getPersonaQuery;
        }

        public async Task<IList<PersonaViewModel>> GetByUserId(Guid userId)
        {
            UserEntity user = await _getUserQuery.Execute(userId);

            return _getPersonasQuery.ExecuteByUser(user);
        }

        public async Task<IList<PersonaViewModel>> GetByUsername(string username)
        {
            UserEntity user = await _getUserQuery.Execute(username);

            return _getPersonasQuery.ExecuteByUser(user);
        }

        public async Task<IList<PersonaViewModel>> GetByConversationId(Guid conversationId, CancellationToken cancellationToken)
        {
            ConversationEntity conversation = await _getConversationQuery.Execute(conversationId, cancellationToken);

            return _getPersonasQuery.ExecuteByConversation(conversation);
        }

        public async Task<PersonaViewModel> Create(PostPersonaRequest request, CancellationToken token)
        {
            UserEntity user = await _getUserQuery.Execute(request.UserId);
            IdentityEntity identity = await _createIdentityQuery.Execute(token);
            PersonaEntity persona = await _createPersonaQuery.Execute(identity, user, token);

            await _linkPersonaToUserCommand.Execute(user, persona, token);

            return persona.Adapt<PersonaViewModel>();
        }

        public async Task<PersonaViewModel> Update(PutPersonaRequest personaRequest, Guid personaId, CancellationToken token)
        {
            PersonaEntity persona = await _getPersonaQuery.Execute(personaId, token);
            await _updatePersonaCommand.Execute(persona, personaRequest, token);

            return persona.Adapt<PersonaViewModel>();
        }
    }
}
