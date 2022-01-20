using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using BusinessLogicLayer.DTOs;
namespace BusinessLogicLayer.Interfaces
{
    public interface IFilmPersonService : IDisposable
    {
        Task<List<FilmPersonDTO>> GetFilmPersonAsync();

        Task<List<FilmPersonDTO>> GetFilmPersonByFilmIdAsync(int filmId);

        Task<List<FilmPersonDTO>> GetFilmPersonByPersonIdAsync(int personId);
        Task<List<FilmPersonDTO>> GetFilmPersonByRoleIdAsync(int roleId);

        Task<bool> AnyFilmPersonAsync(Expression<Func<FilmPerson, bool>> expression);

        Task PostFilmPersonAsync(FilmPersonDTO FilmPerson);

        Task PutFilmPersonAsync(FilmPersonDTO FilmPerson);

        Task DeleteFilmPersonAsync(int filmId, int genreId, int roleId);
    }
}
