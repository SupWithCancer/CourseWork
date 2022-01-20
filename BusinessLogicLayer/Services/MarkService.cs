using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services
{
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MarkService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<MarkDTO> GetMarkAsync(int id)
        {
            var Mark = await _unitOfWork.Mark.GetByIdAsync(id);

            return _mapper.Map<MarkDTO>(Mark);
        }

        public async Task<List<MarkDTO>> GetMarksAsync()
        {
            var marks = await _unitOfWork.Mark.GetAllAsync();

            return _mapper.Map<List<MarkDTO>>(marks);
        }

        //public async Task<List<MarkDTO>> GetMarksByNameAsync(string name)
        //{
        //    var Marks = await _unitOfWork.Mark.GetWhereAsync(d => d.Name == name);

        //    return _mapper.Map<List<MarkDTO>>(Marks);
        //}

        public async Task<List<MarkDTO>> GetMarksByFilmIdAsync(int filmId)
        {
            var Marks = await _unitOfWork.Mark.GetWhereAsync(d => d.FilmId == filmId);

            return _mapper.Map<List<MarkDTO>>(Marks);
        }


        public async Task<List<MarkDTO>> GetMarksByUserIdAsync(int userId)
        {
            var Marks = await _unitOfWork.Mark.GetWhereAsync(d => d.UserId == userId);

            return _mapper.Map<List<MarkDTO>>(Marks);
        }


        public async Task<bool> AnyMarksAsync(Expression<Func<Mark, bool>> expression)
        {
            return await _unitOfWork.Mark.AnyExistingAsync(expression);
        }

        public async Task PostMarkAsync(MarkDTO mark)
        {
            await _unitOfWork.Mark.AddAsync(_mapper.Map<Mark>(mark));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteMarkAsync(int id)
        {
            var Mark = await _unitOfWork.Mark.GetByIdAsync(id);
            if (Mark == null) throw new KeyNotFoundException();

            await _unitOfWork.Mark.DeleteAsync(Mark);
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