using MoviesOnline.Interfaces;

namespace MoviesOnline.Services.UserServices
{
    public class HashPasswordSerivce : IHashPassword
    {
        public string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password);
                }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
