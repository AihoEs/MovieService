

using Microsoft.EntityFrameworkCore;
using MoviesOnline.Models;

namespace MoviesOnline.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMovie>().HasKey(x => new {x.MovieId, x.UserId});

            modelBuilder.Entity<UserMovie>().HasOne(x => x.User).WithMany(x => x.UserMovies).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserMovie>().HasOne(x => x.Movie).WithMany(x => x.UserMovies).HasForeignKey(x => x.MovieId);
        }
    }
}
