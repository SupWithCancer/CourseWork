using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using DataAccessLayer.Interfaces;


namespace BusinessLogicLayer.Services
{
    public class GenreFilmService : IGenreFilmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreFilmService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }
        public async Task<List<GenreFilmDTO>> GetGenreFilmAsync()
        {
            var pairs = await _unitOfWork.GenreFilm.GetAllAsync();

            return _mapper.Map<List<GenreFilmDTO>>(pairs);
        }

        public async Task<List<GenreFilmDTO>> GetGenreFilmByFilmIdAsync(int filmId)
        {
            var pairs = await _unitOfWork.GenreFilm.GetWhereAsync(ir => ir.FilmId == filmId);

            return _mapper.Map<List<GenreFilmDTO>>(pairs);
        }

        public async Task<List<GenreFilmDTO>> GetGenreFilmByGenreIdAsync(int genreId)
        {
            var pairs = await _unitOfWork.GenreFilm.GetWhereAsync(ir => ir.GenreId == genreId);

            return _mapper.Map<List<GenreFilmDTO>>(pairs);
        }

        public async Task<bool> AnyGenreFilmAsync(Expression<Func<GenreFilm, bool>> expression)
        {
            return await _unitOfWork.GenreFilm.AnyExistingAsync(expression);
        }

        public async Task PostGenreFilmAsync(GenreFilmDTO GenreFilm)
        {
            await _unitOfWork.GenreFilm.AddAsync(_mapper.Map<GenreFilm>(GenreFilm));
            await _unitOfWork.SaveAsync();
        }

        public async Task PutGenreFilmAsync(GenreFilmDTO GenreFilm)
        {
            await _unitOfWork.GenreFilm.UpdateAsync(_mapper.Map<GenreFilm>(GenreFilm));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGenreFilmAsync(int filmId, int genreId)
        {
            var GenreFilm = await _unitOfWork.GenreFilm
                .GetFirstOrDefaultAsync(ir => ir.FilmId == filmId &&
                                              ir.GenreId == genreId);
            if (GenreFilm == null) throw new KeyNotFoundException();

            await _unitOfWork.GenreFilm.DeleteAsync(GenreFilm);
            await _unitOfWork.SaveAsync();
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