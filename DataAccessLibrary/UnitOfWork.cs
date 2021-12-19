using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FilmLibraryContext db;
        public UnitOfWork() => db = new();

        private IRepository<int, User> usersRepository;
        private IRepository<int, Film> filmsRepository;
        private IRepository<int, FilmPerson> filmPersonRepository;
        private IRepository<int, Mark> marksRepository;
        private IRepository<int, Genre> genresRepository;
        private IRepository<int, GenreFilm> genreFilmRepository;
        private IRepository<int, Person> personRepository;
        private IRepository<int, Role> roleRepository;
        private IRepository<int, Comment> commentsRepository;


        public IRepository<int, User> User
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new EFRepository<int, User, FilmLibraryContext>(db);
                return usersRepository;
            }
        }

     

        public IRepository<int, GenreFilm> GenreFilm
        {
            get
            {
                if (genreFilmRepository == null)
                    genreFilmRepository = new EFRepository<int, GenreFilm, FilmLibraryContext>(db);
                return genreFilmRepository;
            }
        }
        public IRepository<int, Film> Film
        {
            get
            {
                if (filmsRepository == null)
                    filmsRepository = new EFRepository<int, Film, FilmLibraryContext>(db);
                return filmsRepository;
            }
        }

        public IRepository<int, Person> Person
        {
            get
            {
                if (personRepository == null)
                    personRepository = new EFRepository<int, Person, FilmLibraryContext>(db);
                return personRepository;
            }
        }

        public IRepository<int, Mark> Mark
        {
            get
            {
                if (marksRepository == null)
                    marksRepository = new EFRepository<int, Mark, FilmLibraryContext>(db);
                return marksRepository;
            }
        }

        public IRepository<int, Genre> Genre
        {
            get
            {
                if (genresRepository == null)
                    genresRepository = new EFRepository<int, Genre, FilmLibraryContext>(db);
                return genresRepository;
            }
        }

        public IRepository<int, Role> Role
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new EFRepository<int, Role, FilmLibraryContext>(db);
                return roleRepository;
            }
        }

        public IRepository<int, FilmPerson> FilmPerson
        {
            get
            {
                if (filmPersonRepository == null)
                    filmPersonRepository = new EFRepository<int, FilmPerson, FilmLibraryContext>(db);
                return filmPersonRepository;
            }
        }
        public IRepository<int, Comment> Comment
        {
            get
            {
                if (commentsRepository == null)
                    commentsRepository = new EFRepository<int, Comment, FilmLibraryContext>(db);
                return commentsRepository;
            }
        }

        public async Task SaveAsync() => await db.SaveChangesAsync();

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    db.Dispose();

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
