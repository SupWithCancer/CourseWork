namespace DataAccessLayer.Models;

public class User
{
    public User()
    {
        Comments = new List<Comment>();
        Marks = new List<Mark>();
    }

    public List<Comment> Comments { get; set; }

    public List<Mark> Marks { get; set; }
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
  


}