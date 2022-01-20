using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IFilmService : IDisposable
    {
        Task<FilmDTO> GetFilmAsync(int id);

        Task<List<FilmDTO>> GetFilmsAsync();

        Task<List<FilmDTO>> GetFilmsByNameAsync(string name);
        Task<List<FilmDTO>> GetFilmsByDescriptionAsync(string description);

        Task<List<FilmDTO>> GetFilmsByParameters(string? name, string? description);
        Task<List<FilmDTO>> GetFilmsByYearAsync(int year);

        Task<List<FilmDTO>> GetFilmsByThemeAsync(string theme);

        Task<bool> AnyFilmsAsync(Expression<Func<Film, bool>> expression);

        Task PostFilmAsync(FilmDTO film);

        Task PutFilmAsync(int id, FilmDTO film);

        Task DeleteFilmAsync(int id);
    }
}
