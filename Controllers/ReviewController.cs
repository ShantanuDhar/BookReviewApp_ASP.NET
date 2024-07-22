using Microsoft.AspNetCore.Mvc;
using BookReviewApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BookReviewApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            model.UserID = userId.Value;
            model.CreatedDate = DateTime.Now;

            // Call stored procedure
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC AddReview {model.Book.Title}, {model.UserID}, {model.ReviewText}, {model.Rating}");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetReviewsByBookTitle()
        {
            return View();
        }

        public async Task<IActionResult> GetReviews(string bookTitle)
        {
            List<Review> reviews;

            if (string.IsNullOrEmpty(bookTitle))
            {
                reviews = await _context.Reviews.FromSqlRaw("EXEC GetReviews").ToListAsync();
            }
            else
            {
                reviews = await _context.Reviews.FromSqlInterpolated($"EXEC GetReviewsByBookTitle {bookTitle}").ToListAsync();
                ViewBag.BookTitle = bookTitle;
            }

            ViewBag.Reviews = reviews;
            return View(reviews);
        }
    }
}
