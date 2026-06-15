using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? QrToken { get; set; }
        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
        public ICollection<CoffeeUsage>? CoffeeUsages { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<CoffeeTransaction>? CoffeeTransactions { get; set; }
    }
}