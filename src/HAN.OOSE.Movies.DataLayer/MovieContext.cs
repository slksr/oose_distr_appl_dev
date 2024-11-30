using HAN.OOSE.Movies.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HAN.OOSE.Movies.DataLayer
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }

}
