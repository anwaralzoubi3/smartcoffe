using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Shop
    {
        [Key]
        public int ShopID { get; set; }

        public string ShopName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? LogoUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}