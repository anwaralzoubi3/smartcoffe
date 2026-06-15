using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Plan
    {
        [Key]
        public int PlanID { get; set; }

        [Required]
        public string PlanName { get; set; } = string.Empty;

        // اسم المقهى
        public string CoffeeShopName { get; set; } = string.Empty;

        public int CupsPerMonth { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
    }
}