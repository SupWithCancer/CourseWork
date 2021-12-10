using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<int, User> User { get; }

        IRepository<int, Film> Film { get; }

        IRepository<int, UserRole> UserRole { get; }

        IRepository<int, Mark> Mark { get; }

        IRepository<int, Genre> Genre { get; }

        IRepository<int, GenreFilm> GenreFilm { get; }

        IRepository<int, FilmPerson> FilmPerson { get; }

        IRepository<int, Person> Person { get; }

        IRepository<int, Role> Role { get; }

        IRepository<int, Comment> Comment { get; }
 
        Task SaveAsync();
    }
}
