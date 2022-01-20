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
    public class FilmPersonService : IFilmPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilmPersonService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }
        public async Task<List<FilmPersonDTO>> GetFilmPersonAsync()
        {
            var pairs = await _unitOfWork.FilmPerson.GetAllAsync();

            return _mapper.Map<List<FilmPersonDTO>>(pairs);
        }

        public async Task<List<FilmPersonDTO>> GetFilmPersonByFilmIdAsync(int filmId)
        {
            var pairs = await _unitOfWork.FilmPerson.GetWhereAsync(ir => ir.FilmId == filmId);

            return _mapper.Map<List<FilmPersonDTO>>(pairs);
        }

        public async Task<List<FilmPersonDTO>> GetFilmPersonByPersonIdAsync(int personId)
        {
            var pairs = await _unitOfWork.FilmPerson.GetWhereAsync(ir => ir.PersonId == personId);

            return _mapper.Map<List<FilmPersonDTO>>(pairs);
        }

        public async Task<List<FilmPersonDTO>> GetFilmPersonByRoleIdAsync(int roleId)
        {
            var pairs = await _unitOfWork.FilmPerson.GetWhereAsync(ir => ir.RoleId == roleId);

            return _mapper.Map<List<FilmPersonDTO>>(pairs);
        }

        public async Task<bool> AnyFilmPersonAsync(Expression<Func<FilmPerson, bool>> expression)
        {
            return await _unitOfWork.FilmPerson.AnyExistingAsync(expression);
        }

        public async Task PostFilmPersonAsync(FilmPersonDTO FilmPerson)
        {
            await _unitOfWork.FilmPerson.AddAsync(_mapper.Map<FilmPerson>(FilmPerson));
            await _unitOfWork.SaveAsync();
        }

        public async Task PutFilmPersonAsync(FilmPersonDTO FilmPerson)
        {
            await _unitOfWork.FilmPerson.UpdateAsync(_mapper.Map<FilmPerson>(FilmPerson));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFilmPersonAsync(int filmId, int personId, int roleId)
        {
            var FilmPerson = await _unitOfWork.FilmPerson
                .GetFirstOrDefaultAsync(ir => ir.FilmId == filmId &&
                                              ir.PersonId == personId && 
                                              ir.RoleId == roleId);
            if (FilmPerson == null) throw new KeyNotFoundException();

            await _unitOfWork.FilmPerson.DeleteAsync(FilmPerson);
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