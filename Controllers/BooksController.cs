using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moment3.Data;
using Moment3.Models;

namespace Moment3.Controllers
{
    public class BooksController : Controller
    {

        private readonly BooksDbContext _context;

        //Skickar med booksDbCOntext in i kontrollern
        public BooksController(BooksDbContext context)
        {
            _context = context;
        }
        
        public IActionResult index()
        {//Skickar alla böcker och flörfattare till vien
            var books = _context.Book.Include(b => b.Authors).ToList();
            return View(books);
        }

        //Create -- GET
        public IActionResult Create()
        {
            return View();
        }

        //Create -- POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book, string authors)
        {
            if (ModelState.IsValid)
            { //om modellen är giltig

                if (!string.IsNullOrEmpty(authors))
                { //om skild från null/empty
                    //Delar vid komma tecken
                    var authorNames = authors.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var name in authorNames)
                    { //För varje enskilt i authoNames

                        //Kollar om författare redan finns i databasen
                        var author = _context.Author.FirstOrDefault(a => a.Name == name);

                        //Om författare ej fanns så läggs den till i db
                        if (author == null)
                        {
                            author = new Author { Name = name };
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

        //------------------------------------------DELETE---------------------------------------------------------//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = _context.Book.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);

            //Om boken finns, ta bort författare och boken.  
            if (book != null)
            {
                book.Authors.Clear();
                _context.Book.Remove(book);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------------EDIT/PUT-------------------------------------------------//
        //GET
        public IActionResult Edit(int id)
        {
            var book = _context.Book //Laddar in bok med id, samt författare
                .Include(b => b.Authors) //hämtar föftattare från tabellen
                .FirstOrDefault(b => b.Id == id); 

            if (book != null)
            { //om skilt från null
                var authorAll = string.Join(", ", book.Authors.Select(a => a.Name)); //Sätter till en komma indelad lista
                ViewBag.Authors = authorAll; //viewbag
            }
            return View(book);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book, string authors)
        {

            if (ModelState.IsValid){ //Om valid
                var ChoosenBook = _context.Book //hämtar bok och författare
                    .Include(b => b.Authors)
                    .FirstOrDefault(b => b.Id == id);

                if (ChoosenBook != null) //Om skild från null
                {
                    ChoosenBook.Title = book.Title; //uppdatera titel
                    ChoosenBook.Description = book.Description; //Uppdatera beskrivning
                    ChoosenBook.Authors.Clear(); //rensa författare.
                }

                if (!string.IsNullOrEmpty(authors)) //om författare "stringen" inte är tom eller null
                {   
                    var authorNames = authors.Split(',', StringSplitOptions.RemoveEmptyEntries); //dela upp med komma
                    
                    foreach (var name in authorNames)
                    {   //Om den finns i db så hämta den
                        var author = _context.Author.FirstOrDefault(a => a.Name == name);
                        //Om författare ej finns i db så skapa.
                        if (author == null)
                        {
                            author = new Author { Name = name };
                            _context.Author.Add(author);
                            _context.SaveChanges();
                        }

                        ChoosenBook?.Authors.Add(author);
                    }
                }

                _context.SaveChanges();
                //Skicka tillbaka till index.(lista över blöcker.)
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

//----------------------------------Visa info----------------------------------------//

    public IActionResult Information(int id)
{
    var book = _context.Book //alla böcker från tabellen
        .Include(b => b.Authors) //inkludera författare från author tabell
        .FirstOrDefault(b => b.Id == id); //första med rätt id

    return View(book);
}


    }
}