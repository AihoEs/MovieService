using Microsoft.IdentityModel.Tokens;
using MoviesOnline.Interfaces;
using MoviesOnline.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MoviesOnline.Services.CookieServices
{
    public class TokenGenerator : ITokenGenerator
    {
        public IConfiguration config { get; set; }
        private readonly TimeSpan TimeTokenLife = TimeSpan.FromHours(1);

        public TokenGenerator(IConfiguration config)
        {
            this.config = config;
            
        }

        public string TokenGenerate(User account)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserName", account.Name),
                new Claim("Password", account.HashPassword),
                new Claim("Email", account.Email)
            };
            var jwtToken = new JwtSecurityToken(expires: DateTime.UtcNow.Add(TimeTokenLife), claims: claims, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])), SecurityAlgorithms.HmacSha256));
            return  new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
