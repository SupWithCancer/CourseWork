
namespace DataAccessLayer.Models
{
    public class Role
    {
       public int Id { get; set; }

       public string Title { get; set; }

       public string Description { get; set; }

        public List<FilmPerson> FilmPersonPairs { get; set; }

        public Role() => FilmPersonPairs = new();
    }
}
