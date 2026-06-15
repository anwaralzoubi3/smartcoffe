using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace project.Controllers
{
    [AuthSessionFilter]
    public class HomeController : Controller
    {
        private readonly SmartCoffeeDbContext _context;

        public HomeController(SmartCoffeeDbContext context)
        {
            _context = context;
        }

        // ===================== DASHBOARD =====================
        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var user = _context.Users
                .FirstOrDefault(x => x.UserID == userId);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var subscription = _context.UserSubscriptions
                .Include(x => x.Plan)
                .FirstOrDefault(x =>
                    x.UserID == userId &&
                    x.IsActive);

            var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var usage = _context.CoffeeUsages
                .FirstOrDefault(x =>
                    x.UserId == userId &&
                    x.Month == currentMonth);

            int used = usage?.UsedCups ?? 0;
            int limit = subscription?.Plan.CupsPerMonth ?? 0;
            int remaining = Math.Max(0, limit - used);

            int percentage = limit == 0 ? 0 : (used * 100) / limit;

            ViewBag.Subscription = subscription;
            ViewBag.Used = used;
            ViewBag.Limit = limit;
            ViewBag.Remaining = remaining;
            ViewBag.Percentage = percentage;

            return View(user);
        }

        // ===================== PROFILE =====================
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var user = _context.Users
                .FirstOrDefault(x => x.UserID == userId);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            return View(user);
        }

        // ===================== PLANS =====================
        public IActionResult Plans()
        {
            var plans = _context.Plans
                .Where(x => x.IsActive)
                .ToList();

            return View(plans);
        }

      
        // ===================== SEARCH =====================
        public IActionResult Search()
        {
            var products = _context.CoffeeProducts
                .Where(x => x.IsActive)
                .ToList();

            return View(products);
        }

        // ===================== STATIC PAGES =====================
        public IActionResult MyQr() => View();
        public IActionResult Rewards() => View();
        public IActionResult Notifications() => View();
        public IActionResult Settings() => View();
    }
}