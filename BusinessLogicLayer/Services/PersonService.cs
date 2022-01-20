using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUoWFactory uowFactory, IMapper mapper)
        {
            _unitOfWork = uowFactory.CreateUoW();
            _mapper = mapper;
        }

        public async Task<PersonDTO> GetPersonAsync(int id)
        {
            var person = await _unitOfWork.Person.GetByIdAsync(id);

            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<List<PersonDTO>> GetPersonsAsync()
        {
            var persons = await _unitOfWork.Person.GetAllAsync();

            return _mapper.Map<List<PersonDTO>>(persons);
        }

        //public async Task<List<PersonDTO>> GetPersonsByNameAsync(string name)
        //{
        //    var Persons = await _unitOfWork.Person.GetWhereAsync(d => d.Name == name);

        //    return _mapper.Map<List<PersonDTO>>(Persons);
        //}

        public async Task<bool> AnyPersonsAsync(Expression<Func<Person, bool>> expression)
        {
            return await _unitOfWork.Person.AnyExistingAsync(expression);
        }

        public async Task PostPersonAsync(PersonDTO Person)
        {
            await _unitOfWork.Person.AddAsync(_mapper.Map<Person>(Person));
            await _unitOfWork.SaveAsync();
        }

        public async Task PutPersonAsync(int id, PersonDTO Person)
        {
            await _unitOfWork.Person.UpdateAsync(_mapper.Map<Person>(Person));
            await _unitOfWork.SaveAsync();
        }
        public async Task<List<PersonDTO>> GetPersonsByNameAsync(string name)
        {
            var persons = await _unitOfWork.Person.GetWhereAsync(d => d.Name.Contains(name));

            return _mapper.Map<List<PersonDTO>>(persons);
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _unitOfWork.Person.GetByIdAsync(id);
            if (person == null) throw new KeyNotFoundException();

            await _unitOfWork.Person.DeleteAsync(person);
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