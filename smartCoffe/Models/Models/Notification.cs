using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        public int UserID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;
    }
}