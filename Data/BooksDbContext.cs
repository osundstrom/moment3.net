using Moment3.Models;
using Microsoft.EntityFrameworkCore;




namespace Moment3.Data {
    public class BooksDbContext : DbContext{
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }
        

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}   