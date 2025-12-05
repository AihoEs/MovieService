using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MoviesOnline.Data;
using MoviesOnline.Interfaces;
using MoviesOnline.Models;
using MoviesOnline.Services.CookieServices;
using MoviesOnline.Services.UserServices;
using System.Threading.Tasks;

namespace MoviesOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataBaseContext _con;
        private readonly IIDGenerate _id;
        private readonly IHashPassword _hash;
        
        private readonly ITokenGenerator _token;
        

        public AuthController(DataBaseContext con, IIDGenerate id, IHashPassword hash, ITokenGenerator token)
        {
            _con = con;
            _id = id;
            _hash = hash;
            _token = token;
            
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Register([FromBody] UserRegDTO user)
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
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogDTO user)
        {
            var dbUser = _con.Users.FirstOrDefault(x => x.Name == user.UserName);

            if(dbUser == null)
                throw new UnauthorizedAccessException("Invalid credentials");
            if(!_hash.VerifyPassword(user.PasswordHash, dbUser.HashPassword ))
                throw new UnauthorizedAccessException("Invalid credentials");

            var jwt = _token.TokenGenerate(dbUser);
            return Ok(jwt);
        }


        public class UserRegDTO
        {
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
            public string Email { get; set; }
        }
        public class UserLogDTO
        {
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
           
        }
    }
}
