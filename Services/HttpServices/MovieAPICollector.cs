
using System.Net.Http;
using System.Threading.Tasks;
using MoviesOnline.Interfaces;
using Newtonsoft.Json;

namespace MoviesOnline.Services.HttpServices
{
    public class MovieAPICollector : IMovieAPICollector
    {
        private readonly HttpClient _http;
        private readonly IConfiguration config;

        public MovieAPICollector(HttpClient http, IConfiguration config)
        {
            _http = http;
            this.config = config;
        }
        public async Task<List<OmdbMovie>> SearchMovieAsync(string title)
        {
            var encoded = Uri.EscapeDataString(title);
            var url = $"http://www.omdbapi.com/?apikey={config["Api:Key"]}&s={encoded}";
            var response = await _http.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<OmdbSearchResponse>(response);
            return result.Search;
        }
        public async Task<OmdbMovie> GetMovieByIdAsync(string imdbId)
        {
            var url = $"http://www.omdbapi.com/?apikey={config["Api:Key"]}&i={Uri.EscapeDataString(imdbId)}";
            var json = await _http.GetStringAsync(url);
            return JsonConvert.DeserializeObject<OmdbMovie>(json);
        }
    }
}
