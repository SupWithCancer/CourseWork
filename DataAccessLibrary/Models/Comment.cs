namespace DataAccessLayer.Models;

public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public int FilmId { get; set; }

    public Film Film { get; set; }

    public string? Body { get; set;}
}
