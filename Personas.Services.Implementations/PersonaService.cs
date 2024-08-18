using Chat.CQRS.Queries.Done;
using Core.Common.DataAccess.SharedEntities.Chats;
using Core.Common.DataAccess.SharedEntities.Users;
using Identities.SQRS;
using Persona.CQRS.Queries;
using Persona.Models.Personas;
using Users.CQRS;

namespace Personas.Services.Implementations
{
    public class PersonaService
    {
        private readonly GetUserQuery _getUserQuery;
        private readonly GetConversationQuery _getConversationQuery;
        private readonly GetPersonasQuery _getPersonasQuery;
        private readonly CreatePersonaQuery _createPersonaQuery;
        private readonly CreateIdentityQuery _createIdentityQuery;

        public PersonaService
        (
            GetUserQuery getUserQuery,
            GetConversationQuery getConversationQuery,
            GetPersonasQuery getPersonasQuery,
            CreatePersonaQuery createPersonaQuery,
            CreateIdentityQuery createIdentityQuery
        )
        {
            _getUserQuery = getUserQuery;
            _getConversationQuery = getConversationQuery;
            _getPersonasQuery = getPersonasQuery;
            _createPersonaQuery = createPersonaQuery;
            _createIdentityQuery = createIdentityQuery;
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

        public async Task<IList<PersonaViewModel>> GetByConversationId(Guid conversationId)
        {
            ConversationEntity conversation = await _getConversationQuery.Execute(conversationId);

            return _getPersonasQuery.ExecuteByConversation(conversation);
        }

        public async Task<PersonaViewModel> Create(PostPersonaRequest request)
        {
            UserEntity user = await _getUserQuery.Execute(request.UserId);
            IdentityEntity identity = _createIdentityQuery.Execute();
            PersonaEntity persona = _createPersonaQuery.Execute(identity, user);

            throw new NotImplementedException("Link persona to user");

            return persona.ToViewModel();
        }
    }
}
