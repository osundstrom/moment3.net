using System.ComponentModel.DataAnnotations;


namespace Moment3.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        // Flera böcker för en förtfattare
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}