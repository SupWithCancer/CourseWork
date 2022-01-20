using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<GenreDTO> GetGenreAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);

            return _mapper.Map<GenreDTO>(genre);
        }

        public async Task<List<GenreDTO>> GetGenresAsync()
        {
            var genres = await _unitOfWork.Genre.GetAllAsync();

            return _mapper.Map<List<GenreDTO>>(genres);
        }

        //public async Task<List<GenreDTO>> GetGenresByGroupIdAsync(int groupId)
        //{
        //    var genres = await _unitOfWork.Genre.GetWhereAsync(d => d.GroupId == groupId);

        //    return _mapper.Map<List<genreDTO>>(genres);
        //}

        public async Task<bool> AnyGenresAsync(Expression<Func<Genre, bool>> expression)
        {
            return await _unitOfWork.Genre.AnyExistingAsync(expression);
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) _unitOfWork.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}