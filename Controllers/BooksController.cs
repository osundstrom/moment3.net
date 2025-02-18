using Microsoft.AspNetCore.Mvc;
using Moment3.Models;

namespace Moment3.Controllers {
    public class BooksController: Controller {
        public IActionResult index() {
            return View();
        }


        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book) {
            return View();
        }


        


    }
}