using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moment3.Data;
using Moment3.Models;

namespace Moment3.Controllers{
    public class BooksController : Controller{

        private readonly BooksDbContext _context;

        public BooksController(BooksDbContext context)
        {
            _context = context;
        }

        public IActionResult index(){
            var books = _context.Book.Include(b => b.Authors).ToList();
            return View(books);
        }

        //Create -- GET
        public IActionResult Create(){
            return View();
        }
        
        //Create -- POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, string authors){
            if (ModelState.IsValid){ //om modellen är giltig

                if (!string.IsNullOrEmpty(authors)){ //om skild från null/empty
                    //Delar vid komma tecken
                    var authorNames = authors.Split(',', StringSplitOptions.RemoveEmptyEntries);
                                             
                    foreach (var name in authorNames){ //För varje enskilt i authoNames
                        
                        //Kollar om författare redan finns i databasen
                        var author = _context.Author.FirstOrDefault(a => a.Name == name);
                        
                        //Om författare ej fanns så läggs den till i db
                        if (author == null){
                            author = new Author {Name = name};
                            _context.Author.Add(author);
                            _context.SaveChanges();
                        }
                        //Koppplas med boken, så en författare kan ha skrivit flera böcker. 
                        book.Authors.Add(author);
                    }
                }
                //Lägg till boken
                _context.Book.Add(book);
                _context.SaveChanges();

                //Skickas  till alla böcker.
                return RedirectToAction("Index");
            }

            return View(book);
        }




    }
}