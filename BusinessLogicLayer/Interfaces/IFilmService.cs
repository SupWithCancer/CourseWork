using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IFilmService : IDisposable
    {
        Task<FilmDTO> GetFilmAsync(int id);

        Task<List<FilmDTO>> GetFilmAsync();

        //Task<List<FilmDTO>> GetDishesByCuisineIdAsync(int cuisineId);

        //Task<List<FilmDTO>> GetDishesByCategoryIdAsync(int categoryId);

        Task<bool> AnyFilmAsync(Expression<Func<Film, bool>> expression);

        Task PostFilmAsync(FilmDTO film);

        Task PutFilmAsync(int id, FilmDTO film);

        Task DeleteFilmAsync(int id);
    }
}
