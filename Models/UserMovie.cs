namespace MoviesOnline.Models
{
    public class UserMovie
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int MovieId { get; set; }
        public string MovieImDb { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
