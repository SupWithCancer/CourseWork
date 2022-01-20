using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<CommentDTO> GetCommentAsync(int id)
        {
            var comment = await _unitOfWork.Comment.GetByIdAsync(id);

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<List<CommentDTO>> GetCommentsAsync()
        {
            var comments = await _unitOfWork.Comment.GetAllAsync();

            return _mapper.Map<List<CommentDTO>>(comments);
        }

        //public async Task<List<CommentDTO>> GetCommentsByNameAsync(string name)
        //{
        //    var Comments = await _unitOfWork.Comment.GetWhereAsync(d => d.Name == name);

        //    return _mapper.Map<List<CommentDTO>>(Comments);
        //}

        public async Task<List<CommentDTO>> GetCommentsByFilmIdAsync(int filmId)
        {
            var comments = await _unitOfWork.Comment.GetWhereAsync(d => d.FilmId == filmId);

            return _mapper.Map<List<CommentDTO>>(comments);
        }


        public async Task<List<CommentDTO>> GetCommentsByUserIdAsync(int userId)
        {
            var comments = await _unitOfWork.Comment.GetWhereAsync(d => d.UserId == userId);

            return _mapper.Map<List<CommentDTO>>(comments);
        }


        public async Task<bool> AnyCommentsAsync(Expression<Func<Comment, bool>> expression)
        {
            return await _unitOfWork.Comment.AnyExistingAsync(expression);
        }

        public async Task PostCommentAsync(CommentDTO Comment)
        {
            await _unitOfWork.Comment.AddAsync(_mapper.Map<Comment>(Comment));
            await _unitOfWork.SaveAsync();
        }

        public async Task PutCommentAsync(int id, CommentDTO Comment)
        {
            await _unitOfWork.Comment.UpdateAsync(_mapper.Map<Comment>(Comment));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var Comment = await _unitOfWork.Comment.GetByIdAsync(id);
            if (Comment == null) throw new KeyNotFoundException();

            await _unitOfWork.Comment.DeleteAsync(Comment);
            await _unitOfWork.SaveAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _unitOfWork.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}