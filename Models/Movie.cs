namespace MoviesOnline.Models
{
    public class Movie
    {
        public int Id { get; set; }           
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbID { get; set; }    
        public string Type { get; set; }      
        public string Poster { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();


    }
}
