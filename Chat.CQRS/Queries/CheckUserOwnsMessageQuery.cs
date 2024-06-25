using ChatUserLink.Services.Interfaces;

namespace Chat.CQRS.Queries
{
    public class CheckUserOwnsMessageQuery
    {
        private readonly IUserAssociationRepository _associationRepository;

        public CheckUserOwnsMessageQuery(IUserAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }

        public async Task<bool> Execute(Guid messageId, Guid userId)
        {
            return await _associationRepository.CheckUserOwnsMessage(userId, messageId);
        }
    }
}
