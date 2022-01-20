using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMarkService : IDisposable
    {
        Task<MarkDTO> GetMarkAsync(int id);

        Task<List<MarkDTO>> GetMarksAsync();

        Task<List<MarkDTO>> GetMarksByFilmIdAsync(int filmId);

        Task<List<MarkDTO>> GetMarksByUserIdAsync(int userId);

        Task<bool> AnyMarksAsync(Expression<Func<Mark, bool>> expression);

        Task PostMarkAsync(MarkDTO Mark);

       

        Task DeleteMarkAsync(int id);
    }
}
