using System.ComponentModel.DataAnnotations;


namespace Moment3.Models{
    public class Book {
        public int Id {get; set;} //Id för bok

        [Required]
        public string? Title {get; set;} //Titel för bok

         [Required]
        public List<Author>? Author {get; set;} //Författare för bok, kan vara flera för en bok

    }
}