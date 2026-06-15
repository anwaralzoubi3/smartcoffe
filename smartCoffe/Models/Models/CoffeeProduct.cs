using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CoffeeProduct
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CupsCost { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public ICollection<CoffeeUsage>? CoffeeUsages { get; set; }
    }
}