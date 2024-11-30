using HAN.OOSE.Movies.BusinessLayer.Models;

namespace HAN.OOSE.Movies.BusinessLayer.Contracts
{
    public interface IMovieService
    {
        Task<MovieDTO> CreateMovie(MovieDTO movie);
        Task UpdateMovie(MovieDTO movie);
        Task DeleteMovie(MovieDTO movie);
        Task<List<MovieDTO>> GetMovies();
    }
}
