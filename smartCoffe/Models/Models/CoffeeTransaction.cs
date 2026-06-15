using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class CoffeeTransaction
    {
        [Key]
        public int TransactionID { get; set; }

        public int UserID { get; set; }

        public int ProductID { get; set; }

        public int CupsUsed { get; set; }

        public DateTime TransactionDate { get; set; }
            = DateTime.UtcNow;

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(ProductID))]
        public CoffeeProduct Product { get; set; } = null!;
    }
}