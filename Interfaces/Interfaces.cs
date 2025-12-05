using MoviesOnline.Models;
using MoviesOnline.Services.HttpServices;
using static MoviesOnline.Controllers.AuthController;

namespace MoviesOnline.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(UserRegDTO dto);
    }
    public interface IIDGenerate
    {
        int IdGenerate();
    }
    public interface IMovieAPICollector
    {
        Task<List<OmdbMovie>> SearchMovieAsync(string title);
    }
    public interface ITokenGenerator
    {
        string TokenGenerate(User account);
    }
    public interface IHashPassword
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
    
    
}
