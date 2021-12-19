using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilmService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<FilmDTO> GetFilmAsync(int id)
        {
            var film = await _unitOfWork.Film.GetByIdAsync(id);

            return _mapper.Map<FilmDTO>(film);
        }

        public async Task<List<FilmDTO>> GetFilmsAsync()
        {
            var films = await _unitOfWork.Film.GetAllAsync();

            return _mapper.Map<List<FilmDTO>>(films);
        }

        //public async Task<List<FilmDTO>> GetFilmsByGenreIdAsync(int categoryId)
        //{
        //    var recipes = await _unitOfWork.Recipes.GetWhereAsync(d => d.CategoryId == categoryId);

        //    return _mapper.Map<List<RecipeDTO>>(recipes);
        //}

        //public async Task<List<RecipeDTO>> GetRecipesByCuisineIdAsync(int cuisineId)
        //{
        //    var recipes = await _unitOfWork.Recipes.GetWhereAsync(d => d.CuisineId == cuisineId);

        //    return _mapper.Map<List<RecipeDTO>>(recipes);
        //}

        public async Task<bool> AnyFilmsAsync(Expression<Func<Film, bool>> expression)
        {
            return await _unitOfWork.Film.AnyExistingAsync(expression);
        }

        public async Task PostFilmAsync(FilmDTO film)
        {
            await _unitOfWork.Film.AddAsync(_mapper.Map<Film>(film));
            await _unitOfWork.SaveAsync();
        }

        public async Task PutFilmAsync(int id, FilmDTO film)
        {
            await _unitOfWork.Film.UpdateAsync(_mapper.Map<Film>(film));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFilmAsync(int id)
        {
            var film = await _unitOfWork.Film.GetByIdAsync(id);
            if (film == null) throw new KeyNotFoundException();

            await _unitOfWork.Film.DeleteAsync(film);
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