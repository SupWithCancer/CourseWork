using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<FilmDTO>> GetFilmsByNameAsync(string name)
        {
            var films = await _unitOfWork.Film.GetWhereAsync(d => d.Name.ToLower().Contains(name.ToLower()) || d.Description.ToLower().Contains(name.ToLower()));


            return _mapper.Map<List<FilmDTO>>(films);
        }

        public async Task<List<FilmDTO>> GetFilmsByThemeAsync(string theme)
        {
            var films = await _unitOfWork.Film.GetWhereAsync(d => d.Theme.ToLower().Contains(theme.ToLower()));

            return _mapper.Map<List<FilmDTO>>(films);
        }
        public async Task<List<FilmDTO>> GetFilmsByDescriptionAsync(string description)
        {
            var films = await _unitOfWork.Film.GetWhereAsync(d => d.Description.ToLower().Contains(description.ToLower()));

            return _mapper.Map<List<FilmDTO>>(films);
        }
        public async Task<List<FilmDTO>> GetFilmsByParameters(
        string? name, string? description)
        {
            var query = _unitOfWork.Film.GetQueryable();

            if (name != null)
                query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));

            
            if (description!= null)
                query = query.Where(d => d.Description.ToLower().Contains(description.ToLower()));
          

            return _mapper.Map<List<FilmDTO>>(await query.ToListAsync());
        }

        public async Task<List<FilmDTO>> GetFilmsByYearAsync(int year)
        {
            var films = await _unitOfWork.Film.GetWhereAsync(d => d.Year.Equals(year));

            return _mapper.Map<List<FilmDTO>>(films);
        }
     

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