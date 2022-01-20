using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreService : IDisposable
    {
        Task<GenreDTO> GetGenreAsync(int id);

        Task<List<GenreDTO>> GetGenresAsync();

        Task<bool> AnyGenresAsync(Expression<Func<Genre, bool>> expression);
    }
}
