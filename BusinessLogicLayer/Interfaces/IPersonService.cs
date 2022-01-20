using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPersonService : IDisposable
    {
        Task<PersonDTO> GetPersonAsync(int id);

        Task<List<PersonDTO>> GetPersonsAsync();

        Task<List<PersonDTO>> GetPersonsByNameAsync(string name);

        Task<bool> AnyPersonsAsync(Expression<Func<Person, bool>> expression);

        Task PostPersonAsync(PersonDTO Person);

        Task PutPersonAsync(int id, PersonDTO Person);

        Task DeletePersonAsync(int id);
    }
}
