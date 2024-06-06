using Authorization.Domain;

namespace Authorization.Services.Interfaces
{
    public interface IValidationService
    {
        public bool Validate(UserEntity savedUser, UserRequest userRequest);
    }
}
