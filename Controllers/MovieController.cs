using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesOnline.Data;
using MoviesOnline.Models;
using MoviesOnline.Services.CookieServices;
using MoviesOnline.Services.HttpServices;
using MoviesOnline.Services.UserServices;

namespace MoviesOnline.Controllers
{
    [ApiController]
    [Route("Movie")]
    [TypeFilter(typeof(GlobalExceptionFilter))]
    
    public class MovieController : ControllerBase
    {
        private readonly MovieAPICollector _Movie;
        private readonly DataBaseContext _con;
        private readonly IdGenerator _id;
        private readonly HashPasswordSerivce _hash;
        private readonly MovieConverter _MovieConverter;
        private readonly TokenGenerator _token;


        public MovieController(DataBaseContext con, IdGenerator id, HashPasswordSerivce hash, TokenGenerator token, MovieConverter movieConverter, MovieAPICollector movie)
        {
            _con = con;
            _id = id;
            _hash = hash;
            _token = token;
            _MovieConverter = movieConverter;
            _Movie = movie;

        }


        

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            
            var movie = await _Movie.SearchMovieAsync(title);
            if (movie == null || movie.Count == 0)
                throw new Exception("No movies found");
            return Ok(movie);
        }
        //ДОДЕЛАТЬ В САМОМ КОНЦЕ
       /* [HttpPost("MovieFavorite")]
        
        public async Task<IActionResult> AddToFavorite([FromBody] FavoriteDTO dto)
        {
            var user = await _con.Users.Include(u => u.UserMovies).FirstOrDefaultAsync(u => u.Id == dto.UserId);

            if (user == null)
                return NotFound("User not found");

            var ImdbMovie = await _Movie.GetMovieByIdAsync(dto.ImDb);
            var movie = await _MovieConverter.MovieConvert(ImdbMovie);

            if(!user.UserMovies.Any(u => u.MovieImDb == dto.ImDb))
            {
                user.UserMovies.Add(movie);
                await _con.SaveChangesAsync();
            }
            return Ok(user.UserMovies);
               

            

            }*/
        public class FavoriteDTO
        {
            public string ImDb { get; set; }
            public int UserId { get; set; }
        }
        }

    }

