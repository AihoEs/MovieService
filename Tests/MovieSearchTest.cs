using Moq;
using MoviesOnline;
using MoviesOnline.Interfaces;
using MoviesOnline.Services.HttpServices;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MovieSearchTest
    {
        [Fact]
        public async Task MovieSearchMock_Should_Get200()
        {

            var mockMovieApi = new Mock<IMovieAPICollector>();

            var FakeResponse = new OmdbSearchResponse
            {
                Search = new List<OmdbMovie>
                {
                    new OmdbMovie { Title = "Inception", Year = "2010", imdbID = "tt1375666" }
                }
            };

            mockMovieApi
                .Setup(x => x.SearchMovieAsync("Inception"))
                .ReturnsAsync(FakeResponse.Search);

            var result = await mockMovieApi.Object.SearchMovieAsync("Inception");

            Assert.NotNull(result);
            Assert.Equal("Inception", result[0].Title);



        }
    }
}
