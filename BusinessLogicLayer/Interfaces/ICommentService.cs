using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentService : IDisposable
    {
        Task<CommentDTO> GetCommentAsync(int id);

        Task<List<CommentDTO>> GetCommentsAsync();

        Task<List<CommentDTO>> GetCommentsByFilmIdAsync(int filmId);

        Task<List<CommentDTO>> GetCommentsByUserIdAsync(int userId);

        Task<bool> AnyCommentsAsync(Expression<Func<Comment, bool>> expression);

        Task PostCommentAsync(CommentDTO comment);

        Task PutCommentAsync(int id, CommentDTO comment);

        Task DeleteCommentAsync(int id);
    }
}
