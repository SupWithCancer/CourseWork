using DataAccessLayer.Models;

namespace BusinessLogicLayer.DTOs
{
    public class LoginRes
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public LoginRes(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name; 
            Token = token;
        }
    }
}