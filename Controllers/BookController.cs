using System;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApp.Controllers
{
	public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book model)
        {
            if (ModelState.IsValid)
            {
                await _context.AddBook(model.Title, model.Author, model.Genre);
                return RedirectToAction("GetBooks", "Book");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.GetBooks();
            return View(books);
        }

    }
}

