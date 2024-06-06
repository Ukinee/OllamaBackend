﻿using Authorization.Domain;
using Authorization.Services.Interfaces;

namespace Authorization.Services.Implementations
{
    public class ValidationService(IPasswordService passwordService) : IValidationService
    {
        public bool Validate(UserEntity savedUser, UserRequest userRequest)
        {
            string password = userRequest.Password;
            string hashedPassword = savedUser.HashedPassword;
            string salt = savedUser.Salt;
            
            return passwordService.ValidatePassword(password, hashedPassword, salt);
        }
    }
}
