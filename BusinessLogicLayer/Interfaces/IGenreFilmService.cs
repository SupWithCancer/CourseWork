using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using BusinessLogicLayer.DTOs;
namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreFilmService : IDisposable
    {
        Task<List<GenreFilmDTO>> GetGenreFilmAsync();

        Task<List<GenreFilmDTO>> GetGenreFilmByFilmIdAsync(int filmId);

        Task<List<GenreFilmDTO>> GetGenreFilmByGenreIdAsync(int genreId);

        Task<bool> AnyGenreFilmAsync(Expression<Func<GenreFilm, bool>> expression);

        Task PostGenreFilmAsync(GenreFilmDTO GenreFilm);

        Task PutGenreFilmAsync(GenreFilmDTO GenreFilm);

        Task DeleteGenreFilmAsync(int filmId, int genreId);
    }
}
