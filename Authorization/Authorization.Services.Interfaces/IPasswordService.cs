namespace Authorization.Services.Interfaces
{
    public interface IPasswordService
    {
        public bool ValidatePassword(string password, string hashedPassword, string salt);
        public void HashPassword(string password, out string hashedPassword, out string salt);
    }
}
