
namespace DataAccessLayer.Models
{
    public class GenreFilm
    {
        public int FilmId { get; set; }

        public Film Film { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }   
    }
}
