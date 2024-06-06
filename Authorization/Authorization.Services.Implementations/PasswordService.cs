using System.Security.Cryptography;
using System.Text;
using Authorization.Services.Interfaces;

namespace Authorization.Services.Implementations
{
    public class PasswordService : IPasswordService
    {
        public void HashPassword(string password, out string hashedPassword, out string salt)
        {
            byte[] saltBytes = GenerateSalt();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] saltedPassword = ConcatenateArray(passwordBytes, saltBytes); 

            byte[] hashedPasswordBytes = SHA256.HashData(saltedPassword);
            byte[] saltedHashedPassword = ConcatenateArray(hashedPasswordBytes, saltBytes);
            
            hashedPassword = Convert.ToBase64String(saltedHashedPassword);
            salt = Convert.ToBase64String(saltBytes);
        }

        public bool ValidatePassword(string password, string hashedPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            byte[] saltedPassword = ConcatenateArray(passwordBytes, saltBytes);
            
            byte[] hashedPasswordBytes = SHA256.HashData(saltedPassword);
            byte[] saltedHashedPassword = ConcatenateArray(hashedPasswordBytes, saltBytes);
            
            return Convert.ToBase64String(saltedHashedPassword) == hashedPassword;
        }

        private static byte[] ConcatenateArray(byte[] first, byte[] second) =>
            first.Concat(second).ToArray();

        private byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(16);
        }
    }
}
