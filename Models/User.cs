namespace MoviesOnline.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
       

        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
