using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookReviewApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var passwordHash = ComputeHash(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == passwordHash);
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserID);
                    HttpContext.Session.SetString("Username", user.Username);
                    TempData["Message"] = "Login successful!";
                    TempData["AlertType"] = "success";
                    // Login successful
                    return RedirectToAction("Index", "Home");

                   
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Logout successful!";
            TempData["AlertType"] = "success";
            return RedirectToAction("Login");
        }
        



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                model.PasswordHash = ComputeHash(model.PasswordHash);
                model.CreatedDate = DateTime.Now;
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ViewBag.Message = "User with this email already exists.";
                    ViewBag.AlertType = "danger"; 
                    return View(model);
                }
                _context.Users.Add(model);
                int a = await _context.SaveChangesAsync();
                //await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC RegisterUser {model.Username}, {model.Email}, {model.PasswordHash}");
                if (a > 0)
                {
                    ViewBag.Message = "Registration successful!";
                    ViewBag.AlertType = "success";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.Message = "Registration unsuccessful!";
                    ViewBag.AlertType = "danger";
                    return View(model);
                }
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }



        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }


    }
}
