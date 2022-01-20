using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmLibraryContext _db;
        public UnitOfWork() => _db = new();

        private IRepository<int, User> _userRepository;
        private IRepository<int, Film> _filmsRepository;
        private IRepository<int, FilmPerson> _film_personRepository;
        private IRepository<int, Mark> _marksRepository;
        private IRepository<int, Genre> _genresRepository;
        private IRepository<int , GenreFilm> _genreFilmRepository;
        private IRepository<int, Person> _personRepository;
        private IRepository<int, Role> _roleRepository;
        private IRepository<int, Comment> _commentsRepository;


        public IRepository<int, User> User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new EFRepository<int, User, FilmLibraryContext>(_db);
                return _userRepository;
            }
        }

     

        public IRepository<int, GenreFilm> GenreFilm
        {
            get
            {
                if (_genreFilmRepository == null)
                    _genreFilmRepository = new EFRepository<int, GenreFilm, FilmLibraryContext>(_db);
                return _genreFilmRepository;
            }
        }
        public IRepository<int, Film> Film
        {
            get
            {
                if (_filmsRepository == null)
                    _filmsRepository = new EFRepository<int, Film, FilmLibraryContext>(_db);
                return _filmsRepository;
            }
        }

        public IRepository<int, Person> Person
        {
            get
            {
                if (_personRepository == null)
                    _personRepository = new EFRepository<int, Person, FilmLibraryContext>(_db);
                return _personRepository;
            }
        }

        public IRepository<int, Mark> Mark
        {
            get
            {
                if (_marksRepository == null)
                    _marksRepository = new EFRepository<int, Mark, FilmLibraryContext>(_db);
                return _marksRepository;
            }
        }

        public IRepository<int, Genre> Genre
        {
            get
            {
                if (_genresRepository == null)
                    _genresRepository = new EFRepository<int, Genre, FilmLibraryContext>(_db);
                return _genresRepository;
            }
        }

        public IRepository<int, Role> Role
        {
            get
            {
                if (_roleRepository == null)
                    _roleRepository = new EFRepository<int, Role, FilmLibraryContext>(_db);
                return _roleRepository;
            }
        }

        public IRepository<int, FilmPerson> FilmPerson
        {
            get
            {
                if (_film_personRepository == null)
                    _film_personRepository = new EFRepository<int, FilmPerson, FilmLibraryContext>(_db);
                return _film_personRepository;
            }
        }
        public IRepository<int, Comment> Comment
        {
            get
            {
                if (_commentsRepository == null)
                    _commentsRepository = new EFRepository<int, Comment, FilmLibraryContext>(_db);
                return _commentsRepository;
            }
        }

 
        public async Task SaveAsync() => await _db.SaveChangesAsync();
        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _db.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
