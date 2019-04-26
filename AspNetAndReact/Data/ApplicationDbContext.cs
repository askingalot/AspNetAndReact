using AspNetAndReact.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetAndReact.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<UserMovie> UserMovie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "It's a Wonderful Life",
                    Year = 1946
                },
                new Movie
                {
                    Id = 2,
                    Title = "His Girl Friday",
                    Year = 1940
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Thin Man",
                    Year = 1934
                }
            );
        }
    }
}
