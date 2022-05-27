namespace Movies.Services{
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;



public interface ICosmosDbService
{
    Task<IEnumerable<Movie>> GetMoviesAsync(string query);
    Task<Movie> GetMovieAsync(string id);
    Task AddMovieAsync(Movie Movie);
    Task UpdateMovieAsync(string id, Movie movie);
    Task DeleteMovieAsync(string id);
}
}