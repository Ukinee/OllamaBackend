using Authorization.Services.Interfaces;
using Core.Common.DataAccess.SharedEntities.Users;

namespace Users.CQRS
{
    public class GetUserQuery
    {
        private readonly IUserRepository _userRepository;

        public GetUserQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Execute(Guid userId)
        {
            UserEntity? userEntity = await _userRepository.Find(x => x.Id == userId);

            ArgumentNullException.ThrowIfNull(userEntity);

            return userEntity;
        }

        public async Task<UserEntity> Execute(string username)
        {
            UserEntity? userEntity = await _userRepository.Find(x => x.UserName == username);

            ArgumentNullException.ThrowIfNull(userEntity);

            return userEntity;
        }
    }
}
