using MoviesOnline.Models;

namespace MoviesOnline.Services.HttpServices
{
    public class OmdbMovie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

    
    public class OmdbSearchResponse
    {
        public List<OmdbMovie> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    
    public class MovieConverter
    {
        public Movie MovieConvert(OmdbMovie omdb)
        {
            return new Movie
            {
                Title = omdb.Title,
                Year = omdb.Year,
                ImdbID = omdb.imdbID,
                Type = omdb.Type,
                Poster = omdb.Poster
            };
        }
    }
}

