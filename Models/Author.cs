using System.ComponentModel.DataAnnotations;


namespace Moment3.Models{
    public class Author {
        public int Id {get; set;}  //id

        [Required]
        public string? Name {get; set;} //namn på författare

        public List<Book>? Book {get; set;} //en författare kan ha flera böcker.
    }
}