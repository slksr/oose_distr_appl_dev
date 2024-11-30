using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAN.OOSE.Movies.BusinessLayer.Contracts;
using HAN.OOSE.Movies.BusinessLayer.Models;
using HAN.OOSE.Movies.DataLayer.Contracts;
using HAN.OOSE.Movies.DataLayer.Entities;
using Microsoft.Extensions.Logging;

namespace HAN.OOSE.Movies.BusinessLayer
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IMovieRepository movieRepository, ILogger<MovieService> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task<MovieDTO> CreateMovie(MovieDTO movieDTO)
        {
            // todo: validatie!
            Movie movie = MapToMovie(movieDTO);
            movie = await _movieRepository.Create(movie);
            return movieDTO;
        }

        private Movie MapToMovie(MovieDTO movieDTO)
        {
            return new Movie
            {
                Genre = movieDTO.Genre,
                Title = movieDTO.Title,
                Price = movieDTO.Price,
                Rating = movieDTO.Rating,
                ReleaseDate = movieDTO.ReleaseDate,
            };
        }

        public Task DeleteMovie(MovieDTO movieDTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovie(MovieDTO movieDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieDTO>> GetMovies()
        {
            var movies = await _movieRepository.GetAll();
            var dtos = new List<MovieDTO>();
            foreach (var movie in movies)
            {
                dtos.Add(new MovieDTO
                {
                    Genre=movie.Genre,
                    Title=movie.Title,
                    Price=movie.Price,
                    Rating=movie.Rating,
                    ReleaseDate=movie.ReleaseDate
                });
            }

            return dtos;
        }
    }
}
