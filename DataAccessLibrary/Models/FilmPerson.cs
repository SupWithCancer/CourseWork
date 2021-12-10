
namespace DataAccessLayer.Models
{
    public class FilmPerson
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int FilmId { get; set; }

        public Film Film { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }


    }
}
