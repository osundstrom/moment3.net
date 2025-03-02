using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moment3.Data;
using Moment3.Models;

namespace Moment3.Controllers {
    public class BooksController: Controller {

        private readonly BooksDbContext _context;

        public BooksController(BooksDbContext context) {
            _context = context;
        }

        public IActionResult index() {
            return View();
        }


        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book) {

            if(ModelState.IsValid) {
                //
                _context.Add(book);
                _context.SaveChanges();
                
            }
            return View();
        }


        


    }
}