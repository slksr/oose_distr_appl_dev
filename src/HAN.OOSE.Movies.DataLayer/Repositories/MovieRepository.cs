using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAN.OOSE.Movies.DataLayer.Contracts;
using HAN.OOSE.Movies.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HAN.OOSE.Movies.DataLayer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(MovieContext context, ILogger<MovieRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task Delete(int movieId)
        {
            var movie = await _context.Movie.FindAsync(movieId);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movie.ToListAsync();
        }

        public async Task Update(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}
