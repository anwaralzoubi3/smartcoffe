using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using System.Linq;

namespace project.Controllers
{
    public class AuthController : Controller
    {
        private readonly SmartCoffeeDbContext _context;
        private readonly PasswordHasher<User> _hasher = new PasswordHasher<User>();

        public AuthController(SmartCoffeeDbContext context)
        {
            _context = context;
        }

        // ===================== SPLASH =====================
        public IActionResult Splash()
        {
            return View();
        }

        // ===================== ONBOARDING =====================
        public IActionResult Onboarding()
        {
            var completed = HttpContext.Session.GetString("OnboardingCompleted");

            if (completed == "true")
                return RedirectToAction("Welcome");

            return View();
        }

        public IActionResult CompleteOnboarding()
        {
            HttpContext.Session.SetString("OnboardingCompleted", "true");
            return RedirectToAction("Welcome");
        }

        // ===================== WELCOME =====================
        public IActionResult Welcome()
        {
            return View();
        }

        // ===================== REGISTER =====================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string phoneNumber, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password required";
                return View();
            }

            var exists = _context.Users.Any(x => x.Email == email);

            if (exists)
            {
                ViewBag.Error = "Email already exists";
                return View();
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.Now
            };

            user.PasswordHash = _hasher.HashPassword(user, password);

            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("IsLoggedIn", "true");

            return RedirectToAction("Dashboard", "Home");
        }

        // ===================== LOGIN =====================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password required";
                return View();
            }

            var user = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            var result = _hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password);

            if (result != PasswordVerificationResult.Success)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("IsLoggedIn", "true");

            return RedirectToAction("Dashboard", "Home");
        }

        // ===================== FORGOT PASSWORD =====================
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult VerifyOtp()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        // ===================== LOGOUT =====================
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Welcome");
        }
    }
}