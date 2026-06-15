using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace project.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly SmartCoffeeDbContext _context;

        public CoffeeController(SmartCoffeeDbContext context)
        {
            _context = context;
        }

        // ===================== REDEEM COFFEE =====================
        [HttpPost]
        public IActionResult Redeem(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return Unauthorized("User not logged in");

            // 🔥 مهم: Include أفضل لتقليل مشاكل lazy loading
            var subscription = _context.UserSubscriptions
                .FirstOrDefault(x => x.UserID == userId && x.IsActive);

            if (subscription == null)
                return BadRequest("No active subscription");

            var product = _context.CoffeeProducts
                .FirstOrDefault(x => x.ProductID == productId && x.IsActive);

            if (product == null)
                return NotFound("Product not found");

            // 🔥 التحقق من الرصيد
            if (subscription.RemainingCups < product.CupsCost)
                return BadRequest("Not enough cups in your plan ☹️");

            // خصم الرصيد
            subscription.RemainingCups -= product.CupsCost;

            // تسجيل العملية
            var transaction = new CoffeeTransaction
            {
                UserID = userId.Value,
                ProductID = productId,
                CupsUsed = product.CupsCost,
                TransactionDate = DateTime.UtcNow
            };

            _context.CoffeeTransactions.Add(transaction);

            // حفظ في SQLite
            _context.SaveChanges();

            return Ok();
        }
    }
}