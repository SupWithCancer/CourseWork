namespace DataAccessLayer.Models;

public class Film
{
    public Film()
    {
        GenreFilmPairs = new List<GenreFilm>();
        FilmPersonPairs = new List<FilmPerson>();
        Marks = new List<Mark>();
        Comments = new List<Comment>();
       
    }
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
    
    public int Year { get; set; }

    public string ImagePath { get; set; }
    
    public List<Mark> Marks { get; set; }

    public List<Comment> Comments { get; set;}

    public List<GenreFilm> GenreFilmPairs { get; set; }

    public List<FilmPerson> FilmPersonPairs { get; set; }



}
