namespace BusinessLogicLayer.DTOs;

public class CommentDTO
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FilmId { get; set; }

    public string? Body { get; set; }
}
