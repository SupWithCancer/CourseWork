namespace DataAccessLayer.Models;

public class Genre
 {
 public int Id { get; set; }
 public string Name { get; set; }
 public bool IsDeleted { get; set; }
 public string Description { get; set; }

 public List<GenreFilm> GenreFilmPairs { get; set; }

 public Genre() => GenreFilmPairs = new ();

}
