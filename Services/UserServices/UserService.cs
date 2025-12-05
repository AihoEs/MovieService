using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesOnline.Data;
using MoviesOnline.Interfaces;
using MoviesOnline.Models;
using MoviesOnline.Services.CookieServices;
using static MoviesOnline.Controllers.AuthController;

namespace MoviesOnline.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _con;
        private readonly IdGenerator _id;
        private readonly HashPasswordSerivce _hash;

        private readonly TokenGenerator _token;


        public UserService(DataBaseContext con, IdGenerator id, HashPasswordSerivce hash, TokenGenerator token)
        {
            _con = con;
            _id = id;
            _hash = hash;
            _token = token;

        }
        public async Task<User> Register(UserRegDTO user)
        {
            var newUser = new User()
            {
                Name = user.UserName,
                Email = user.Email,
                HashPassword = _hash.HashPassword(user.PasswordHash),
                Id = _id.IdGenerate()

            };

            _con.Users.Add(newUser);
            await _con.SaveChangesAsync();
            return newUser;
        }
        public async Task<string> Login( UserLogDTO user)
        {
            var dbUser = _con.Users.FirstOrDefault(x => x.Name == user.UserName);

            if (dbUser == null)
                throw new Exception("User not authorized");
            if (!_hash.VerifyPassword(user.PasswordHash, dbUser.HashPassword))
                throw new Exception("Invalid credentials");

            var jwt = _token.TokenGenerate(dbUser);
            return jwt;
        }
    }
}
