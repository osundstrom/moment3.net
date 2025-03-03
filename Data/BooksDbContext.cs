using Moment3.Models;
using Microsoft.EntityFrameworkCore;




namespace Moment3.Data {
    //Databaskoppling
    public class BooksDbContext : DbContext{
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }
        
        //Tabeller
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }

    }   

}   