using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace project.Controllers
{
    public class PaymentController : Controller
    {
        private readonly SmartCoffeeDbContext _context;

        public PaymentController(SmartCoffeeDbContext context)
        {
            _context = context;
        }

        // =====================
        // Checkout Page
        // =====================
        [HttpPost]
        public IActionResult Checkout(int planId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var plan = _context.Plans
                .FirstOrDefault(x => x.PlanID == planId);

            if (plan == null)
                return RedirectToAction("Plans", "Home");

            // 🔥 حفظ الباقة في Session (مهم للأمان)
            HttpContext.Session.SetInt32("PlanId", planId);

            return View(plan);
        }

        // =====================
        // Payment Success
        // =====================
        public IActionResult Success()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var planId = HttpContext.Session.GetInt32("PlanId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            if (planId == null)
                return RedirectToAction("Plans", "Home");

            var plan = _context.Plans
                .FirstOrDefault(x => x.PlanID == planId);

            if (plan == null)
                return RedirectToAction("Plans", "Home");

            // =====================
            // إلغاء الاشتراكات القديمة
            // =====================
            var oldSubscriptions = _context.UserSubscriptions
                .Where(x => x.UserID == userId.Value && x.IsActive)
                .ToList();

            foreach (var sub in oldSubscriptions)
            {
                sub.IsActive = false;
            }

            // =====================
            // إنشاء اشتراك جديد
            // =====================
            var subscription = new UserSubscription
            {
                UserID = userId.Value,
                PlanID = plan.PlanID,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                RemainingCups = plan.CupsPerMonth,
                IsActive = true
            };

            _context.UserSubscriptions.Add(subscription);

            // =====================
            // إنشاء سجل الاستخدام الشهري
            // =====================
            var currentMonth = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                1);

            var usageExists = _context.CoffeeUsages.Any(x =>
                x.UserId == userId.Value &&
                x.Month == currentMonth);

            if (!usageExists)
            {
                _context.CoffeeUsages.Add(new CoffeeUsage
                {
                    UserId = userId.Value,
                    UsedCups = 0,
                    MonthlyLimit = plan.CupsPerMonth,
                    Month = currentMonth
                });
            }

            _context.SaveChanges();

            // 🔥 حذف PlanId من Session بعد النجاح
            HttpContext.Session.Remove("PlanId");

            return View(plan);
        }

        // =====================
        // Payment Cancel
        // =====================
        public IActionResult Cancel()
        {
            return View();
        }
    }
}