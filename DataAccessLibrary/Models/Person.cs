
namespace DataAccessLayer.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }


        public string ImagePath { get; set; }

        public List<FilmPerson> FilmPersonPairs { get; set; }

        public Person() => FilmPersonPairs = new();

    }
}
