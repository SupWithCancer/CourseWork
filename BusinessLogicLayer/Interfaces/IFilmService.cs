using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IFilmService : IDisposable
    {
        Task<FilmDTO> GetFilmAsync(int id);

        Task<List<FilmDTO>> GetFilmsAsync();

 
        Task<bool> AnyFilmsAsync(Expression<Func<Film, bool>> expression);

        Task PostFilmAsync(FilmDTO film);

        Task PutFilmAsync(int id, FilmDTO film);

        Task DeleteFilmAsync(int id);
    }
}
