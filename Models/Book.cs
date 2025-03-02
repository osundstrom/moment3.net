using System.ComponentModel.DataAnnotations;


namespace Moment3.Models{
  public class Book {
    public int Id {get; set;}

    [Required]
    public string? Title {get; set;}

    [Required]
    public string? Description {get; set;}

    public int AuthorId {get; set;}
    public Author? Author {get; set;}
}
}