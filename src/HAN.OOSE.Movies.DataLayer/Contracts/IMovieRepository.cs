using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAN.OOSE.Movies.DataLayer.Entities;

namespace HAN.OOSE.Movies.DataLayer.Contracts
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAll();
        Task<Movie> Create(Movie movie);
        Task Update(Movie movie);
        Task Delete(int movieId);
    }
}
