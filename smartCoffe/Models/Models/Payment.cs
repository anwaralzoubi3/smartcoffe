using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        public int UserID { get; set; }

        public int PlanID { get; set; }

        public decimal Amount { get; set; }

        public string PayPalOrderId { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}