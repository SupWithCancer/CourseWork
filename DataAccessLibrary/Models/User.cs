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

    public string Password { get; set; }

    public int UserRoleId { get; set; }

    public UserRole UserRoles { get; set; }
}